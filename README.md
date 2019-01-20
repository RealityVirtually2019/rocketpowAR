## Welcome! 

We are Team rocketpowAR from the Reality Virtually Hackathon

We're kicking off 2019 with an amazing MIT project utilizing Brain Computer Interface and Magic Leap Technology to create an at-home physical therapy application.

## Built With  

Hardware
OpenBCI Ganglion (4 channel) 

Software 
OpenBCI GUI 
Unity build for Magic Leap 
Node.js


## Plugins Used

* [Unity Socket.io Client Plugin](https://github.com/dp0ch/Unity-SocketIO-Client)
*

## What the project does

*Summary*
Muscle contraction -> electrodes -> amplifier (OpenBCI Ganglion) -> Bluetooth Simblee -> local Node.js server stream -> channel parsing/selection/interpretation/threshold set -> if threshold reached -> data sent through socket.io handshake -> unity socket.io client plugin -> Magic Leap (THE ROCKET FLIES) 

## Why the project is useful
The initial application for RocketPowAR centers around muscle rehabilitation and physical therapy. Recruiting and using the correct muscle group(s) during an exercise is a crucial part of physical therapy to strengthen weak areas and support injured structures as well as a cruitial step in preparing for the integration of a prosthetic. 

Current physical therapy devices such as TENS or e-Stim use electrical signals either to bypass a self directed recruitment (you choosing to turn a muscle “on”) by sending an electrical current through electrodes and into the muscle to stimulate nerve pathways. This is incredibly unhelpful as users can’t develop an understanding of how to recruit the muscle themselves, so the moment they stop using the TENS/e-Stim device (TENS/e-Stim sessions last 10-20 minutes maybe 1/weekly), they cannot do the rehab.

These devices are also very cumbersome to set up, difficult to use, and the at-home kits do not support at-home users. We wanted to fill in the gaps to help. So many people live with pain that they brush off as "a part of getting old". It does not have to be that way.

Also users get to fly a rocket and terraform Mars. Physical therapy is a drag so by creating an engaging environment that can get the users wanting to keep up their streaks, and also making the exercise fun to do, users are more likely to actually follow their prescriped rehab protocols. 


## How we managed to make it work 
*Explaination* 
The OpenBCI uses the OpenBCI Ganglion (set as an EMG) to referece the relevative contraction of a muscle group and fly a rocket.

We used electrodes (OpenBCI purple, white, grey with 2 electrodes being +3 and -3 and then a reference electrode) to get a muscle signal to the OpenBCI Ganglion board. This board acted as a signal amplifier and used a Simplee BLE Radio module. The signals were picked up over bluetooth by a sever and stream set up through terminal on a Mac (the Bluetooth-ing for OpenBCI products on Windows wouldn't work even with a BLED 112, 4.0 CSR, virtual machine, running Unbuntu through a flash drive/virtual environment, or hacking the USB drivers so for bluetooth to work you really need a Mac). 

On the first Mac we set up (1) a live stream in terminal window of the signal picked up by the Ganglion, (2) a client.js file to initiate the handshake, (3) our WebServer.js which parsed out the relevant channel data. (See a example https://imgur.com/a/J67oZc0 of why we had to parse out the signal this way - each of the 4 channels would pick up signal, and then each channel would return an array of size 4). 

Still on this first Mac we found a threshold that would indicate that the muscle was active. We chose this by using the OpenBCI GUI to visualize FFT processed live data and a pyplot to visualize non live data https://imgur.com/a/ivxesRg to figure out which channel was the relevant signal and what numbers of microvolts/microsecond to expect within the stream. This threshold is then used as the trigger within an if statement of the stream.js. Once the threshold is reached, the local HTP server is initiated and the "go" signal is sent to the client. This client runs on a second laptop and takes the "go" as a trigger event for Unity. This trigger event activates the Rocket thruster animation which allows the rocket to take off. The rocket then follows the user's arm as they raise their arm completing the rehab exercise, and travels towards a distant Mars.  The client lives on the Magic Leap too. 

## Project isn't working first checks 
* Is everyone on the same wifi
* Are you using the right version on Unity
* What channels are you using on the Ganglion and have you parsed out the corresponding one 

* You also need to cycle the Ganglion on and off each time you launch the server
* Make sure that on the first Mac you have both the data stream and client running simultaneously (client receives the raw data and stream will be what prints your console.Log confirmation of Tense/relaxed 
* try lowering the threshold 

## Contributors 
Sophia Batchelor
Brian Jordan
Rogue Fong 
Alexandria Heston 
Jake Rutkowski
