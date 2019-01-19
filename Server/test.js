const io = require('socket.io')();

console.log("Starting fake local server. Fake: Ganglion Found");

io.on("connection", (socket) => {
  console.log("BrainConnected");
    setInterval(() => {
      for (let i = 0; i < 4; i++) {
        let sampleMin = 0.0200;
        let sampleMax = 0.120;
        let fakeNumber = (Math.random() * (sampleMax - sampleMin) + sampleMin).toFixed(8);
        socket.emit("channel-data", {channelID: i, value: fakeNumber});
        console.log(`Channel ${i}: ${fakeNumber} Volts.`);
      }
    }, 50);
})
;
io.listen(3000);
