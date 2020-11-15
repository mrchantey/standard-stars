const jimp = require('jimp');
const fs = require('fs-extra');


async function createSpriteSheet({ sprites, numColsOut, numRowsOut, widthOut, heightOut, invert, normalize }) {
	const imgWidth = sprites[0].bitmap.width
	const imgHeight = sprites[0].bitmap.height
	const sheetWidth = numColsOut * imgWidth
	const sheetHeight = numRowsOut * imgHeight
	const img = await jimp.create(sheetWidth, sheetHeight)

	for (let i = 0; i < sprites.length; i++) {
		const col = i % numColsOut
		const row = Math.floor(i / numColsOut)
		const x = col * imgWidth
		const y = row * imgHeight
		img.blit(sprites[i], x, y)
	}
	if (widthOut !== undefined && heightOut !== undefined)
		img.scaleToFit(widthOut, heightOut)
	// img.resize()
	if (invert === true)
		img.invert()
	if (normalize === true)
		img.normalize()
	return img
}


async function importImageSequence({ pathIn }) {
	const promises = fs
		.readdirSync(pathIn)
		.sort()
		.map(f => jimp.read(`${pathIn}/${f}`))
	const images = await Promise.all(promises)
	return images
}


async function generateSignatures() {
	const args = {
		pathIn: "./input/signatures",
		pathOut: "./output/signatures",
		numColsOut: 4,
		numRowsOut: 16,
		widthOut: 4096,
		heightOut: 4096,
		// invert:true,
		normalize:true
	}

	const dirs = await fs.readdirSync(args.pathIn)
	const sigDatas = dirs.map(d => {
		const split = d.split("-")
		const id = parseInt(split[0])
		const numLoopFrames =parseInt(split[1])
		return {
			...args,
			id,
			pathIn: `${args.pathIn}/${d}`,
			pathOut: `${args.pathOut}/${id}.png`,
			numLoopFrames
		}
	})

	const sigDatasSimple = await Promise.all(sigDatas.map(async sd => {
		const sprites = await importImageSequence(sd)
		const spriteSheet = await createSpriteSheet({ ...sd, sprites })
		await spriteSheet.write(sd.pathOut)
		console.log(`signature created - ${sd.id}`);
		return {
			id: sd.id,
			numFrames: sprites.length,
			numLoopFrames: sd.numLoopFrames,
		}
	}))

	// const jsonData = {
	// 	numCols: args.numColsOut,
	// 	numRows: args.numRowsOut,
	// 	signatureDatas: sigDatasSimple
	// }
	const jsonData = {
		Items:sigDatasSimple
	}
	fs.writeFileSync(`${args.pathOut}/data.json`, JSON.stringify(jsonData, null, 2))
}

generateSignatures()