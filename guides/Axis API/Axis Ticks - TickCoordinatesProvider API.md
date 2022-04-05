# Axis Ticks - TickCoordinatesProvider API
TickCoordinatesProvider API is a part of Axis which is defined by `ISCIAxisCore.tickCoordinatesProvider` and is responsible for converting `ISCITickProvider.ticks` (provided by [TickProvider API](#axis-ticks---tickprovider-and-deltacalculator-api.html)) into coordinates on a screen.

By default each axis uses `SCIDefaultTickCoordinatesProvider` which just converts data ticks into pixel coordinates using `ISCICoordinateCalculator` provided by axis. If you want to customize this process you can extend this class and override `-[ISCIAxisProviderBase update]` to update `SCITickCoordinates`:

<div class="code-snippet-tabs">
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'objectivec')">OBJECTIVE-C</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'swift')">SWIFT</button>
  <button class="code-snippet-tab" onclick="showCodeFor(event, 'cs')">XAMARIN</button>
</div>
<div class="code-snippet" id="objectivec">
    @interface CustomTickCoordinatesProvider : SCIDefaultTickCoordinatesProvider
    @end

    @implementation CustomTickCoordinatesProvider

    - (void)update {
        [super update];
        
        SCITickCoordinates *tickCoordinates = self.tickCoordinates;
        // minorTickCoords contains coordinates of minor ticks on screen
        SCIFloatValues *minorTickCoords = tickCoordinates.minorTickCoordinates;
        // majorTickCoords contains coordinates of major ticks on screen
        SCIFloatValues *majorTickCoords = tickCoordinates.majorTickCoordinates;
        
        // TODO: Provide minor and major Tick Coordinates
    }

    @end
    ...
    axis.tickCoordinatesProvider = [CustomTickCoordinatesProvider new];
</div>
<div class="code-snippet" id="swift">
    class CustomTickCoordinatesProvider: SCIDefaultTickCoordinatesProvider {
        override func update() {
            super.update()

            let tickCoordinates = self.tickCoordinates!
            // minorTickCoords contains coordinates of minor ticks on screen
            let minorTickCoords = tickCoordinates.minorTickCoordinates
            // majorTickCoords contains coordinates of major ticks on screen
            let majorTickCoords = tickCoordinates.majorTickCoordinates
            
            // TODO: Provide minor and major Tick Coordinates
        }
    }
    ...
    axis.tickCoordinatesProvider = CustomTickCoordinatesProvider()
</div>
<div class="code-snippet" id="cs">
    class CustomTickCoordinatesProvider : SCIDefaultTickCoordinatesProvider
    {
        public override void Update()
        {
            base.Update();

            // minorTickCoords contains coordinates of minor ticks on screen
            var minorTickCoords = this.TickCoordinates.MinorTickCoordinates;
            // majorTickCoords contains coordinates of major ticks on screen
            var majorTickCoords = this.TickCoordinates.MajorTickCoordinates;

            // TODO: Provide minor and major Tick Coordinates
        }
    }
    ...
    axis.TickCoordinatesProvider = new CustomTickCoordinatesProvider();
</div>
