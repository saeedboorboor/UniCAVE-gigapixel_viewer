"# UniCAVE-gigapixel_viewer" 
Used UniCAVE plug-in from https://unicave.discovery.wisc.edu/ to design a prefab for the Reality Deck (RD) https://labs.cs.sunysb.edu/labs/vislab/reality-deck-home/

The script named "NewBehaviorScript.cs" is a naive implementation of a  gigapixel viewer for the RD. 
The script takes in the an input directory of tiles (TODO: allow user to browse directory) and loads only the number of tiles that fits into the display. 
Variable "zoomfactor" allows for some form of zooming (buggy - still needs to be fixed). 

TODO: -> Interaction
-> Fix edge case bugs