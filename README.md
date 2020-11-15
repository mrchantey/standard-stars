# Standard Stars 
WIP for Anna Madeleine

# Unity Setup Notes

- StandardStars -> StandardStars6DOF
	- Move Camera Rig to Pose Aggregated Camera Rig -> Aggregator Parent -> AggregatedPose
	- Add Event Listener to On Reset Button Click: ArOrigin-> Reset


- StandardStars6DOF PC -> Mac
	- Add Team Id - V5H6L32832
	- Add Bundle Identifier - org.chantey.standardstars
	- Add Camera Permissions

# Signature Import Requirements

- each animation must have uniform dimensions, however its ok for this to vary between animations
- all images in png format
- naming conventions of images must be uniform across all signatures i.e "img_001.png" etc
- the number of frames in an animation should not exceed 64
- the number of 'complete' frames should be counted, to be looped
- each signature should have an id associated which can later be related to the observer
- the images for each signature must be placed in a folder with the following format 'id-completeframecount', i.e. "1-5"


# Mechanics
- Plate views
	- randomly placed on a point on a sphere of radius 0.5 meters.
	- could be placed in a circle instead to avoid people craning their necks etc.
- Hot-cold targeting system
	

# To Do
- Match 


## Dependencies
- [Ahoy](https://github.com/mrchantey/ahoy.unity/raw/master/Package-Builds/Ahoy.unitypackage)
- [Ahoy.Shaders](https://github.com/mrchantey/ahoy.unity/raw/master/Package-Builds/Ahoy.Shaders.unitypackage)
- [Starchart3d](https://github.com/mrchantey/starchart3d/raw/master/starchart3d.unitypackage)
