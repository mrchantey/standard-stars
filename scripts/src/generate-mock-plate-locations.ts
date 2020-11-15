
const decMin = -90
const decMax = -32

const raMin = 0
const raMax = 24

const numEntries = 100
const numObservers = 2

function lerp(a:number,b:number,t:number){
	return a + (b - a)*t
}


const entries = []

for(let i = 0; i < numEntries; i++){
	const entry = {
		dec: lerp(decMin,decMax,Math.random()),
		ra: Math.random() * 24,
		id: Math.floor(Math.random() * numObservers)+1
	}
	entries.push(entry)	
}

const unityFriendly = {
	Items:entries
}

Deno.writeTextFileSync("output/plateLocations.json",JSON.stringify(unityFriendly,null,2))