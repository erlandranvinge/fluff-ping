<%@ Page Title="Home Page" Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="fluff_ping._Default" %>

<html>
    <head><title>Fluff-ping</title></head>
    <body>
        <h2>Fluff-ping, pinging from the cloud.</h2>
        <pre>
            <b>Usage:</b> <a href="http://fluff-ping.apphb.com/?url1=www.google.se&url2=www.yahoo.com">http://fluff-ping.apphb.com/?url1=www.google.se&url2=www.yahoo.com</a>
            =>
                        
            {
                "results": [
                    {
                        "url": "http://www.google.se",
                        "status": "OK",
                        "time": 45
                    },
                    {
                        "url": "http://www.yahoo.com",
                        "status": "OK",
                        "time": 346
                    }
                ]
            }
        </pre>
    </body>
</html>