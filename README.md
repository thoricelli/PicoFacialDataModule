# PicoFacialDataModule

A VRCFaceTracking module that connects to the `picofacialdatadaemon` via UDP.

## Running

1. Download the module ZIP from the releases.
2. In VRCFaceTracking go to Module Registry > Press the plus at the top.
3. Select the ZIP you downloaded.
4. Run the `picofacialdatadaemon`, or install it via Magisk.

### Babble

For those using a Babble face tracker, you can disable the face tracking for this module:
1. In explorer go to:
```shell
%appdata%/VRCFaceTracking/CustomLibs/61ee1324-fd45-42f1-9636-8e28717cf6db/
```
2. Open `PicoFacialDataModule.json` with a text editor of choice.
3. Disable face tracking, like so:
```shell
{
  "DisableEyeTracking": false,
  "DisableFaceTracking": true
}
```