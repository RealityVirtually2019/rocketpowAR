const io = require('socket.io')();
const Ganglion = require("openbci-ganglion");
const ganglion = new Ganglion();

ganglion.once("ganglionFound", peripheral => {
  console.log("Ganglion found");
  // Stop searching for BLE devices once a ganglion is found.
  ganglion.searchStop();

  io.on("connection", (socket) => {
    console.log("Client connected");
    ganglion.on("sample", sample => {
      /** Work with sample */
      console.log(sample.sampleNumber); // this is where we can put the "at threshold" lsl marker
      for (let i = 0; i < ganglion.numberOfChannels(); i++) {
        socket.emit("channel-data", {channelID: i, value: sample.channelData[i].toFixed(8)});
        console.log("Channel " + (i) + /*we changed this from i+1*/ ": " + sample.channelData[i].toFixed(8) + " Volts.");
      }
    });
  });

  ganglion.once("ready", () => {
    ganglion.streamStart();
  });
  ganglion.connect(peripheral);
});

// Start scanning for BLE devices
ganglion.searchStart();

io.listen(3000);
