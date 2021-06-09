# Light Echoes
WIP for Anna Madeleine

# Unity Setup Notes

## Testing
- feel free to rotate camera to south for testing, just be sure to put it back before shipping:
	- Heirachy -> Standard Stars -> Aggregated Camera Rig
		- Inspector -> Transform -> Rotation -> y: 180
- Windows - DirectX will not work
	- Project Settings -> Player -> Other Settings
		- Auto Graphics API: false
		- Graphics APIs for Windows -> #1: OpenGLES3
						

## iOS Build
- Build Settings -> iOS
- Project Settings -> XR Plug-in Management -> ARKit: true
- Assets -> Materials -> Starchart - 2D - Cam Tex -> InvertY: true
- Assets -> Materials -> Starchart - 3D - Cam Tex -> InvertY: true
- Assets -> Materials -> Starchart - 3D - Constellation -> ZTest: Always
- Assets -> Materials -> Starchart - 3D - Star -> ZTest: Always

## Android Build
- Build Settings -> Android
- Project Settings -> XR Plug-in Management -> ARCore: true
- Assets -> Materials -> Starchart - 2D - Cam Tex -> InvertY: false
- Assets -> Materials -> Starchart - 3D - Cam Tex -> InvertY: false
- Assets -> Materials -> Starchart - 3D - Constellation -> ZTest: GreaterEqual
- Assets -> Materials -> Starchart - 3D - Star -> ZTest: GreaterEqual

## Export iOS in Xcode
1. Product -> Build For -> Any iOS Device
1. Product -> Archive
1. Organizer -> Distribute App --> Development
	- App Thinning: None
	- Rebuild from Bitcode: true
	- Include Manifest for over-the-air installation: false
1. [More Info](https://wiki.genexus.com/commwiki/servlet/wiki?34616,HowTo%3A+Create+an+.ipa+file+from+XCode)

# Signature Import Requirements

- each animation must have uniform dimensions, however its ok for this to vary between animations
- all images in png format
- naming conventions of images must be uniform across all signatures i.e "img_001.png" etc
- the number of frames in an animation should not exceed 64
- the number of 'complete' frames should be counted, to be looped
- each signature should have an id associated which can later be related to the observer
- the images for each signature must be placed in a folder with the following format 'id-completeframecount', i.e. "1-5"

## Dependencies
- [Ahoy](https://github.com/mrchantey/ahoy.unity/raw/master/Package-Builds/Ahoy.unitypackage)
- [Ahoy.Shaders](https://github.com/mrchantey/ahoy.unity/raw/master/Package-Builds/Ahoy.Shaders.unitypackage)
- [Starchart3d](https://github.com/mrchantey/starchart3d/raw/master/starchart3d.unitypackage)
- [Ahoy.AR](https://github.com/mrchantey/ahoy.unity/raw/master/Package-Builds/Ahoy.AR.unitypackage)
