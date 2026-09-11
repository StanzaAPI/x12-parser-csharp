# Healthcare EDI ANSI X12 Parser (837P / 835 / 271) — .NET / C# SDK

[![NuGet version](https://img.shields.io/nuget/v/StanzaApi.X12Parser.svg)](https://www.nuget.org/packages/StanzaApi.X12Parser/)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)
[![Stanza API](https://img.shields.io/badge/Powered%20by-Stanza-blue)](https://stanzaapi.com)

> Zero-regex parser converting raw Healthcare ANSI X12 EDI text into strongly-typed hierarchical JSON with client-side HIPAA DLP guardrails.

Official high-performance .NET client library for **Healthcare EDI ANSI X12 Parser (837P / 835 / 271)**, built on the [Stanza Micro-API Network](https://stanzaapi.com). Fully compatible with .NET Standard 2.0, .NET 6.0, .NET 7.0, and .NET 8.0+.

* 🌐 **Online Interactive Sandbox:** [Test your inputs live](https://stanzaapi.com/tools/x12-parser)
* 📚 **API Reference & Schemas:** [View documentation on Stanza](https://stanzaapi.com/tools/x12-parser)
* ⚡ **Platform Overview:** [Explore the Stanza Developer Network](https://stanzaapi.com)

---

## 📦 Installation

```bash
dotnet add package StanzaApi.X12Parser
```

---

## 🚀 Quickstart

```csharp
using System;
using System.Threading.Tasks;
using StanzaApi.X12Parser;

class Program
{
    static async Task Main()
    {
        // Initialize client (reads STANZA_API_KEY from environment if not passed)
        var client = new X12ParserClient();

        // Perform deterministic verification
        string responseJson = await client.ValidateAsync("ISA*00*          *00*          *ZZ*SUBMITTER      *ZZ*RECEIVER       *230915*1000*^*00501*000000001*0*T*:~GS*HC*SUBMITTER*RECEIVER*20230915*1000*1*X*005010X222A1~ST*837*0001*005010X222A1~BHT*0019*00*CLAIM001*20230915*1000*CH~NM1*41*2*SUBMITTER*****46*123456789~PER*IC*EDI DEPT*TE*8005551212~NM1*40*2*RECEIVER*****46*987654321~HL*1**20*1~PRV*BI*PXC*207Q00000X~NM1*85*2*CLINIC*****XX*1999999999~HL*2*1*22*0~NM1*IL*1*SYNTHETIC*PATIENT****MI*SYN123456~CLM*CLM001*150.00***11:B:1*Y*A*Y*Y~HI*BK:J0100~LX*1~SV1*HC:99213*150.00*UN*1***1~DTP*472*D8*20230910~SE*15*0001~GE*1*1~IEA*1*000000001~");
        Console.WriteLine(responseJson);
    }
}
```

---

## 📄 Example Response

```json
{
  "success": true,
  "data": {
    "transaction_type": "837P",
    "control_number": "0001",
    "total_charge": 150,
    "patient": {
      "last_name": "SYNTHETIC",
      "first_name": "PATIENT"
    },
    "claims": [
      {
        "claim_id": "CLM001",
        "amount": 150,
        "service_lines": [
          {
            "code": "99213",
            "charge": 150
          }
        ]
      }
    ]
  }
}
```

---

## ⚙️ Configuration

Pass options directly to the `X12ParserClient` constructor:

```csharp
var client = new X12ParserClient(
    apiKey: "your_api_key_here",
    baseUrl: "https://api.stanzaapi.com/x12-parser",
    tier: "sandbox" // "enterprise" routes to https://secure.api.stanzaapi.com (AWS BAA secure plane)
);
```

---

## 🔗 Useful Links

* [Healthcare EDI ANSI X12 Parser (837P / 835 / 271) Interactive Sandbox](https://stanzaapi.com/tools/x12-parser)
* [Stanza Developer Directory](https://stanzaapi.com)
* [Source Code & Issue Tracker](https://github.com/StanzaAPI/x12-parser-csharp)

## 📄 License

MIT © Stanza — Powered by [Stanza](https://stanzaapi.com).
