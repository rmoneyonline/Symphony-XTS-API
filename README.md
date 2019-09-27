# Symphony XTS API

C# client of the Symphony XTS API developed by Symphony Fintech, a leading technology provider for capital market players with key focus on automated trading, OEMS, market connectivity and cross-asset front-to-back office trading solutions.

## Requirements
- .Net Framework 4.5.1 and above
- Windows 7 and above

## Dependencies
- [WebSocket4Net](https://www.nuget.org/packages/WebSocket4Net) (Version 0.15.2 or above)
- [Json.Net](https://github.com/JamesNK/Newtonsoft.Json) (Version 12.0.0.0 or above)
- [EngineIoClientDotNet](https://github.com/Quobject/EngineIoClientDotNet) (Version 1.0.2 or above)

## Usage

Since the XTS API comes in two parts namely, the Interactive (Trading) API and the Market Data API the C# client too comes in two parts.

To call the Interactive API please use the below code:
```csharp
XTSInteractive interactive = new XTSInteractive(USER_ID, URL);
//raise the events
interactive.Interactive += OnInteractive;
interactive.Json += OnJson;
interactive.Exception += OnException;
interactive.ConnectionState += OnConnection;

//login to the API
InteractiveLoginResult login = await interactive.LoginAsync<InteractiveLoginResult>(PASSWORD, INTERACTIVE_KEY);

//and connect to the socket
if (login != null)
{
  interactive.ConnectToSocket())
}
```

Once successfully logged in you can request the account informations as shown below

```csharp
//pull the orders placed during the day
await interactive?.GetOrderAsync();
//pull the trades taken during the day
await interactive.GetTradesAsync();
```

You can also place, modify or cancel an order. For example to place an order please use the below code.
```csharp
//Place an order
OrderIdResult orderId = await interactive?.PlaceOrderAsync("NSECM", 2885, "BUY", "LIMIT", 1, 1100.0d, 0.0d, "MIS", "DAY", orderUniqueIdentifier: "uniqueOrderId");
```

Similarly, you can connect to the XTS Market API:

```csharp
XTSMarketData marketData = new XTSMarketData(USER_ID, URL);

marketData.MarketData += OnMarketData;
marketData.ConnectionState += OnConnection;
marketData.Json += OnJson;
marketData.Exception += OnException;

MarketDataLoginResult marketDataLogin = await marketData.LoginAsync<MarketDataLoginResult>(PASSWORD, MARKET_KEY);
if (marketDataLogin != null)
{
   marketData.ConnectToSocket((MarketDataPorts[])Enum.GetValues(typeof(MarketDataPorts)), PublishFormat.JSON, BroadcastMode.Partial))
}
```

And subscribe to a quotes like:
```csharp
await marketData.SubscribeAsync<T>(USER_ID, ((int)MarketDataPorts).ToString(), new Instruments() 
{
  exchangeSegment = (int)ExchangeSegment.NSEFO;
  exchangeInstrumentID = 2885;
});
````

This project does consists of a test application which further demonstrates the relevant methods.
