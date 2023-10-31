// Copyright (c). All rights reserved.
//
// Licensed under the MIT license.

using Shouldly;
using System;
using System.Collections.Generic;
using System.IO;
using Xunit;

namespace AssemblyShader.UnitTests
{
    public sealed class NuGetProjectAssetsFileLoaderTests : TestBase
    {
        [Fact]
        public void TransitiveDependenciesAreCorrectlyLoaded()
        {
            string projectAssetsFileContents = @"{
  ""version"": 3,
  ""targets"": {
    "".NETFramework,Version=v4.7.2"": {
      ""Microsoft.Bcl.HashCode/1.1.1"": {
        ""type"": ""package"",
        ""frameworkAssemblies"": [
          ""System"",
          ""mscorlib""
        ],
        ""compile"": {
          ""ref/net461/Microsoft.Bcl.HashCode.dll"": {}
        },
        ""runtime"": {
          ""lib/net461/Microsoft.Bcl.HashCode.dll"": {
            ""related"": "".xml""
          }
        }
      },
      ""Microsoft.CodeCoverage/17.3.0"": {
        ""type"": ""package"",
        ""compile"": {
          ""lib/net45/Microsoft.VisualStudio.CodeCoverage.Shim.dll"": {}
        },
        ""runtime"": {
          ""lib/net45/Microsoft.VisualStudio.CodeCoverage.Shim.dll"": {}
        },
        ""build"": {
          ""build/netstandard1.0/Microsoft.CodeCoverage.props"": {},
          ""build/netstandard1.0/Microsoft.CodeCoverage.targets"": {}
        }
      },
      ""Microsoft.NET.Test.Sdk/17.3.0"": {
        ""type"": ""package"",
        ""dependencies"": {
          ""Microsoft.CodeCoverage"": ""17.3.0""
        },
        ""compile"": {
          ""lib/net45/_._"": {}
        },
        ""runtime"": {
          ""lib/net45/_._"": {}
        },
        ""build"": {
          ""build/net45/Microsoft.NET.Test.Sdk.props"": {},
          ""build/net45/Microsoft.NET.Test.Sdk.targets"": {}
        },
        ""buildMultiTargeting"": {
          ""buildMultiTargeting/Microsoft.NET.Test.Sdk.props"": {}
        }
      },
      ""Newtonsoft.Json/12.0.1"": {
        ""type"": ""package"",
        ""compile"": {
          ""lib/net45/Newtonsoft.Json.dll"": {
            ""related"": "".pdb;.xml""
          }
        },
        ""runtime"": {
          ""lib/net45/Newtonsoft.Json.dll"": {
            ""related"": "".pdb;.xml""
          }
        }
      },
      ""Newtonsoft.Json.Bson/1.0.2"": {
        ""type"": ""package"",
        ""dependencies"": {
          ""Newtonsoft.Json"": ""12.0.1""
        },
        ""compile"": {
          ""lib/net45/Newtonsoft.Json.Bson.dll"": {
            ""related"": "".pdb;.xml""
          }
        },
        ""runtime"": {
          ""lib/net45/Newtonsoft.Json.Bson.dll"": {
            ""related"": "".pdb;.xml""
          }
        }
      },
      ""NuGet.Frameworks/6.5.0"": {
        ""type"": ""package"",
        ""compile"": {
          ""lib/net472/NuGet.Frameworks.dll"": {}
        },
        ""runtime"": {
          ""lib/net472/NuGet.Frameworks.dll"": {}
        }
      }
    },
    ""net6.0"": {
      ""Microsoft.Bcl.HashCode/1.1.1"": {
        ""type"": ""package"",
        ""compile"": {
          ""ref/netcoreapp2.1/Microsoft.Bcl.HashCode.dll"": {}
        },
        ""runtime"": {
          ""lib/netcoreapp2.1/Microsoft.Bcl.HashCode.dll"": {
            ""related"": "".xml""
          }
        }
      },
      ""Microsoft.CodeCoverage/17.3.0"": {
        ""type"": ""package"",
        ""compile"": {
          ""lib/netcoreapp1.0/Microsoft.VisualStudio.CodeCoverage.Shim.dll"": {}
        },
        ""runtime"": {
          ""lib/netcoreapp1.0/Microsoft.VisualStudio.CodeCoverage.Shim.dll"": {}
        },
        ""build"": {
          ""build/netstandard1.0/Microsoft.CodeCoverage.props"": {},
          ""build/netstandard1.0/Microsoft.CodeCoverage.targets"": {}
        }
      },
      ""Microsoft.NET.Test.Sdk/17.3.0"": {
        ""type"": ""package"",
        ""dependencies"": {
          ""Microsoft.CodeCoverage"": ""17.3.0"",
          ""Microsoft.TestPlatform.TestHost"": ""17.3.0""
        },
        ""compile"": {
          ""lib/netcoreapp2.1/_._"": {}
        },
        ""runtime"": {
          ""lib/netcoreapp2.1/_._"": {}
        },
        ""build"": {
          ""build/netcoreapp2.1/Microsoft.NET.Test.Sdk.props"": {},
          ""build/netcoreapp2.1/Microsoft.NET.Test.Sdk.targets"": {}
        },
        ""buildMultiTargeting"": {
          ""buildMultiTargeting/Microsoft.NET.Test.Sdk.props"": {}
        }
      },
      ""Microsoft.TestPlatform.ObjectModel/17.3.0"": {
        ""type"": ""package"",
        ""dependencies"": {
          ""NuGet.Frameworks"": ""5.11.0"",
          ""System.Reflection.Metadata"": ""1.6.0""
        },
        ""compile"": {
          ""lib/netcoreapp2.1/Microsoft.TestPlatform.CoreUtilities.dll"": {},
          ""lib/netcoreapp2.1/Microsoft.TestPlatform.PlatformAbstractions.dll"": {},
          ""lib/netcoreapp2.1/Microsoft.VisualStudio.TestPlatform.ObjectModel.dll"": {}
        },
        ""runtime"": {
          ""lib/netcoreapp2.1/Microsoft.TestPlatform.CoreUtilities.dll"": {},
          ""lib/netcoreapp2.1/Microsoft.TestPlatform.PlatformAbstractions.dll"": {},
          ""lib/netcoreapp2.1/Microsoft.VisualStudio.TestPlatform.ObjectModel.dll"": {}
        },
        ""resource"": {
          ""lib/netcoreapp2.1/cs/Microsoft.TestPlatform.CoreUtilities.resources.dll"": {
            ""locale"": ""cs""
          },
          ""lib/netcoreapp2.1/cs/Microsoft.VisualStudio.TestPlatform.ObjectModel.resources.dll"": {
            ""locale"": ""cs""
          },
          ""lib/netcoreapp2.1/de/Microsoft.TestPlatform.CoreUtilities.resources.dll"": {
            ""locale"": ""de""
          },
          ""lib/netcoreapp2.1/de/Microsoft.VisualStudio.TestPlatform.ObjectModel.resources.dll"": {
            ""locale"": ""de""
          },
          ""lib/netcoreapp2.1/es/Microsoft.TestPlatform.CoreUtilities.resources.dll"": {
            ""locale"": ""es""
          },
          ""lib/netcoreapp2.1/es/Microsoft.VisualStudio.TestPlatform.ObjectModel.resources.dll"": {
            ""locale"": ""es""
          },
          ""lib/netcoreapp2.1/fr/Microsoft.TestPlatform.CoreUtilities.resources.dll"": {
            ""locale"": ""fr""
          },
          ""lib/netcoreapp2.1/fr/Microsoft.VisualStudio.TestPlatform.ObjectModel.resources.dll"": {
            ""locale"": ""fr""
          },
          ""lib/netcoreapp2.1/it/Microsoft.TestPlatform.CoreUtilities.resources.dll"": {
            ""locale"": ""it""
          },
          ""lib/netcoreapp2.1/it/Microsoft.VisualStudio.TestPlatform.ObjectModel.resources.dll"": {
            ""locale"": ""it""
          },
          ""lib/netcoreapp2.1/ja/Microsoft.TestPlatform.CoreUtilities.resources.dll"": {
            ""locale"": ""ja""
          },
          ""lib/netcoreapp2.1/ja/Microsoft.VisualStudio.TestPlatform.ObjectModel.resources.dll"": {
            ""locale"": ""ja""
          },
          ""lib/netcoreapp2.1/ko/Microsoft.TestPlatform.CoreUtilities.resources.dll"": {
            ""locale"": ""ko""
          },
          ""lib/netcoreapp2.1/ko/Microsoft.VisualStudio.TestPlatform.ObjectModel.resources.dll"": {
            ""locale"": ""ko""
          },
          ""lib/netcoreapp2.1/pl/Microsoft.TestPlatform.CoreUtilities.resources.dll"": {
            ""locale"": ""pl""
          },
          ""lib/netcoreapp2.1/pl/Microsoft.VisualStudio.TestPlatform.ObjectModel.resources.dll"": {
            ""locale"": ""pl""
          },
          ""lib/netcoreapp2.1/pt-BR/Microsoft.TestPlatform.CoreUtilities.resources.dll"": {
            ""locale"": ""pt-BR""
          },
          ""lib/netcoreapp2.1/pt-BR/Microsoft.VisualStudio.TestPlatform.ObjectModel.resources.dll"": {
            ""locale"": ""pt-BR""
          },
          ""lib/netcoreapp2.1/ru/Microsoft.TestPlatform.CoreUtilities.resources.dll"": {
            ""locale"": ""ru""
          },
          ""lib/netcoreapp2.1/ru/Microsoft.VisualStudio.TestPlatform.ObjectModel.resources.dll"": {
            ""locale"": ""ru""
          },
          ""lib/netcoreapp2.1/tr/Microsoft.TestPlatform.CoreUtilities.resources.dll"": {
            ""locale"": ""tr""
          },
          ""lib/netcoreapp2.1/tr/Microsoft.VisualStudio.TestPlatform.ObjectModel.resources.dll"": {
            ""locale"": ""tr""
          },
          ""lib/netcoreapp2.1/zh-Hans/Microsoft.TestPlatform.CoreUtilities.resources.dll"": {
            ""locale"": ""zh-Hans""
          },
          ""lib/netcoreapp2.1/zh-Hans/Microsoft.VisualStudio.TestPlatform.ObjectModel.resources.dll"": {
            ""locale"": ""zh-Hans""
          },
          ""lib/netcoreapp2.1/zh-Hant/Microsoft.TestPlatform.CoreUtilities.resources.dll"": {
            ""locale"": ""zh-Hant""
          },
          ""lib/netcoreapp2.1/zh-Hant/Microsoft.VisualStudio.TestPlatform.ObjectModel.resources.dll"": {
            ""locale"": ""zh-Hant""
          }
        }
      },
      ""Microsoft.TestPlatform.TestHost/17.3.0"": {
        ""type"": ""package"",
        ""dependencies"": {
          ""Microsoft.TestPlatform.ObjectModel"": ""17.3.0"",
          ""Newtonsoft.Json"": ""9.0.1""
        },
        ""compile"": {
          ""lib/netcoreapp2.1/Microsoft.TestPlatform.CommunicationUtilities.dll"": {},
          ""lib/netcoreapp2.1/Microsoft.TestPlatform.CoreUtilities.dll"": {},
          ""lib/netcoreapp2.1/Microsoft.TestPlatform.CrossPlatEngine.dll"": {},
          ""lib/netcoreapp2.1/Microsoft.TestPlatform.PlatformAbstractions.dll"": {},
          ""lib/netcoreapp2.1/Microsoft.TestPlatform.Utilities.dll"": {},
          ""lib/netcoreapp2.1/Microsoft.VisualStudio.TestPlatform.Common.dll"": {},
          ""lib/netcoreapp2.1/Microsoft.VisualStudio.TestPlatform.ObjectModel.dll"": {},
          ""lib/netcoreapp2.1/testhost.dll"": {
            ""related"": "".deps.json""
          }
        },
        ""runtime"": {
          ""lib/netcoreapp2.1/Microsoft.TestPlatform.CommunicationUtilities.dll"": {},
          ""lib/netcoreapp2.1/Microsoft.TestPlatform.CoreUtilities.dll"": {},
          ""lib/netcoreapp2.1/Microsoft.TestPlatform.CrossPlatEngine.dll"": {},
          ""lib/netcoreapp2.1/Microsoft.TestPlatform.PlatformAbstractions.dll"": {},
          ""lib/netcoreapp2.1/Microsoft.TestPlatform.Utilities.dll"": {},
          ""lib/netcoreapp2.1/Microsoft.VisualStudio.TestPlatform.Common.dll"": {},
          ""lib/netcoreapp2.1/Microsoft.VisualStudio.TestPlatform.ObjectModel.dll"": {},
          ""lib/netcoreapp2.1/testhost.dll"": {
            ""related"": "".deps.json""
          }
        },
        ""resource"": {
          ""lib/netcoreapp2.1/cs/Microsoft.TestPlatform.CommunicationUtilities.resources.dll"": {
            ""locale"": ""cs""
          },
          ""lib/netcoreapp2.1/cs/Microsoft.TestPlatform.CrossPlatEngine.resources.dll"": {
            ""locale"": ""cs""
          },
          ""lib/netcoreapp2.1/cs/Microsoft.VisualStudio.TestPlatform.Common.resources.dll"": {
            ""locale"": ""cs""
          },
          ""lib/netcoreapp2.1/de/Microsoft.TestPlatform.CommunicationUtilities.resources.dll"": {
            ""locale"": ""de""
          },
          ""lib/netcoreapp2.1/de/Microsoft.TestPlatform.CrossPlatEngine.resources.dll"": {
            ""locale"": ""de""
          },
          ""lib/netcoreapp2.1/de/Microsoft.VisualStudio.TestPlatform.Common.resources.dll"": {
            ""locale"": ""de""
          },
          ""lib/netcoreapp2.1/es/Microsoft.TestPlatform.CommunicationUtilities.resources.dll"": {
            ""locale"": ""es""
          },
          ""lib/netcoreapp2.1/es/Microsoft.TestPlatform.CrossPlatEngine.resources.dll"": {
            ""locale"": ""es""
          },
          ""lib/netcoreapp2.1/es/Microsoft.VisualStudio.TestPlatform.Common.resources.dll"": {
            ""locale"": ""es""
          },
          ""lib/netcoreapp2.1/fr/Microsoft.TestPlatform.CommunicationUtilities.resources.dll"": {
            ""locale"": ""fr""
          },
          ""lib/netcoreapp2.1/fr/Microsoft.TestPlatform.CrossPlatEngine.resources.dll"": {
            ""locale"": ""fr""
          },
          ""lib/netcoreapp2.1/fr/Microsoft.VisualStudio.TestPlatform.Common.resources.dll"": {
            ""locale"": ""fr""
          },
          ""lib/netcoreapp2.1/it/Microsoft.TestPlatform.CommunicationUtilities.resources.dll"": {
            ""locale"": ""it""
          },
          ""lib/netcoreapp2.1/it/Microsoft.TestPlatform.CrossPlatEngine.resources.dll"": {
            ""locale"": ""it""
          },
          ""lib/netcoreapp2.1/it/Microsoft.VisualStudio.TestPlatform.Common.resources.dll"": {
            ""locale"": ""it""
          },
          ""lib/netcoreapp2.1/ja/Microsoft.TestPlatform.CommunicationUtilities.resources.dll"": {
            ""locale"": ""ja""
          },
          ""lib/netcoreapp2.1/ja/Microsoft.TestPlatform.CrossPlatEngine.resources.dll"": {
            ""locale"": ""ja""
          },
          ""lib/netcoreapp2.1/ja/Microsoft.VisualStudio.TestPlatform.Common.resources.dll"": {
            ""locale"": ""ja""
          },
          ""lib/netcoreapp2.1/ko/Microsoft.TestPlatform.CommunicationUtilities.resources.dll"": {
            ""locale"": ""ko""
          },
          ""lib/netcoreapp2.1/ko/Microsoft.TestPlatform.CrossPlatEngine.resources.dll"": {
            ""locale"": ""ko""
          },
          ""lib/netcoreapp2.1/ko/Microsoft.VisualStudio.TestPlatform.Common.resources.dll"": {
            ""locale"": ""ko""
          },
          ""lib/netcoreapp2.1/pl/Microsoft.TestPlatform.CommunicationUtilities.resources.dll"": {
            ""locale"": ""pl""
          },
          ""lib/netcoreapp2.1/pl/Microsoft.TestPlatform.CrossPlatEngine.resources.dll"": {
            ""locale"": ""pl""
          },
          ""lib/netcoreapp2.1/pl/Microsoft.VisualStudio.TestPlatform.Common.resources.dll"": {
            ""locale"": ""pl""
          },
          ""lib/netcoreapp2.1/pt-BR/Microsoft.TestPlatform.CommunicationUtilities.resources.dll"": {
            ""locale"": ""pt-BR""
          },
          ""lib/netcoreapp2.1/pt-BR/Microsoft.TestPlatform.CrossPlatEngine.resources.dll"": {
            ""locale"": ""pt-BR""
          },
          ""lib/netcoreapp2.1/pt-BR/Microsoft.VisualStudio.TestPlatform.Common.resources.dll"": {
            ""locale"": ""pt-BR""
          },
          ""lib/netcoreapp2.1/ru/Microsoft.TestPlatform.CommunicationUtilities.resources.dll"": {
            ""locale"": ""ru""
          },
          ""lib/netcoreapp2.1/ru/Microsoft.TestPlatform.CrossPlatEngine.resources.dll"": {
            ""locale"": ""ru""
          },
          ""lib/netcoreapp2.1/ru/Microsoft.VisualStudio.TestPlatform.Common.resources.dll"": {
            ""locale"": ""ru""
          },
          ""lib/netcoreapp2.1/tr/Microsoft.TestPlatform.CommunicationUtilities.resources.dll"": {
            ""locale"": ""tr""
          },
          ""lib/netcoreapp2.1/tr/Microsoft.TestPlatform.CrossPlatEngine.resources.dll"": {
            ""locale"": ""tr""
          },
          ""lib/netcoreapp2.1/tr/Microsoft.VisualStudio.TestPlatform.Common.resources.dll"": {
            ""locale"": ""tr""
          },
          ""lib/netcoreapp2.1/zh-Hans/Microsoft.TestPlatform.CommunicationUtilities.resources.dll"": {
            ""locale"": ""zh-Hans""
          },
          ""lib/netcoreapp2.1/zh-Hans/Microsoft.TestPlatform.CrossPlatEngine.resources.dll"": {
            ""locale"": ""zh-Hans""
          },
          ""lib/netcoreapp2.1/zh-Hans/Microsoft.VisualStudio.TestPlatform.Common.resources.dll"": {
            ""locale"": ""zh-Hans""
          },
          ""lib/netcoreapp2.1/zh-Hant/Microsoft.TestPlatform.CommunicationUtilities.resources.dll"": {
            ""locale"": ""zh-Hant""
          },
          ""lib/netcoreapp2.1/zh-Hant/Microsoft.TestPlatform.CrossPlatEngine.resources.dll"": {
            ""locale"": ""zh-Hant""
          },
          ""lib/netcoreapp2.1/zh-Hant/Microsoft.VisualStudio.TestPlatform.Common.resources.dll"": {
            ""locale"": ""zh-Hant""
          }
        },
        ""build"": {
          ""build/netcoreapp2.1/Microsoft.TestPlatform.TestHost.props"": {}
        }
      },
      ""Newtonsoft.Json/12.0.1"": {
        ""type"": ""package"",
        ""compile"": {
          ""lib/netstandard2.0/Newtonsoft.Json.dll"": {
            ""related"": "".pdb;.xml""
          }
        },
        ""runtime"": {
          ""lib/netstandard2.0/Newtonsoft.Json.dll"": {
            ""related"": "".pdb;.xml""
          }
        }
      },
      ""Newtonsoft.Json.Bson/1.0.2"": {
        ""type"": ""package"",
        ""dependencies"": {
          ""Newtonsoft.Json"": ""12.0.1""
        },
        ""compile"": {
          ""lib/netstandard2.0/Newtonsoft.Json.Bson.dll"": {
            ""related"": "".pdb;.xml""
          }
        },
        ""runtime"": {
          ""lib/netstandard2.0/Newtonsoft.Json.Bson.dll"": {
            ""related"": "".pdb;.xml""
          }
        }
      },
      ""NuGet.Frameworks/6.5.0"": {
        ""type"": ""package"",
        ""compile"": {
          ""lib/netstandard2.0/NuGet.Frameworks.dll"": {}
        },
        ""runtime"": {
          ""lib/netstandard2.0/NuGet.Frameworks.dll"": {}
        }
      },
      ""System.Reflection.Metadata/1.6.0"": {
        ""type"": ""package"",
        ""compile"": {
          ""lib/netstandard2.0/System.Reflection.Metadata.dll"": {
            ""related"": "".xml""
          }
        },
        ""runtime"": {
          ""lib/netstandard2.0/System.Reflection.Metadata.dll"": {
            ""related"": "".xml""
          }
        }
      }
    }
  },
  ""libraries"": {
    ""Microsoft.Bcl.HashCode/1.1.1"": {
      ""sha512"": ""MalY0Y/uM/LjXtHfX/26l2VtN4LDNZ2OE3aumNOHDLsT4fNYy2hiHXI4CXCqKpNUNm7iJ2brrc4J89UdaL56FA=="",
      ""type"": ""package"",
      ""path"": ""microsoft.bcl.hashcode/1.1.1"",
      ""files"": [
        "".nupkg.metadata"",
        "".signature.p7s"",
        ""Icon.png"",
        ""LICENSE.TXT"",
        ""THIRD-PARTY-NOTICES.TXT"",
        ""lib/net461/Microsoft.Bcl.HashCode.dll"",
        ""lib/net461/Microsoft.Bcl.HashCode.xml"",
        ""lib/netcoreapp2.1/Microsoft.Bcl.HashCode.dll"",
        ""lib/netcoreapp2.1/Microsoft.Bcl.HashCode.xml"",
        ""lib/netstandard2.0/Microsoft.Bcl.HashCode.dll"",
        ""lib/netstandard2.0/Microsoft.Bcl.HashCode.xml"",
        ""lib/netstandard2.1/Microsoft.Bcl.HashCode.dll"",
        ""lib/netstandard2.1/Microsoft.Bcl.HashCode.xml"",
        ""microsoft.bcl.hashcode.1.1.1.nupkg.sha512"",
        ""microsoft.bcl.hashcode.nuspec"",
        ""ref/net461/Microsoft.Bcl.HashCode.dll"",
        ""ref/netcoreapp2.1/Microsoft.Bcl.HashCode.dll"",
        ""ref/netstandard2.0/Microsoft.Bcl.HashCode.dll"",
        ""ref/netstandard2.1/Microsoft.Bcl.HashCode.dll"",
        ""useSharedDesignerContext.txt"",
        ""version.txt""
      ]
    },
    ""Microsoft.CodeCoverage/17.3.0"": {
      ""sha512"": ""/xxz+e29F2V5pePtInjbLffoqWVTm60KCX87vSj2laNboeWq65WFJ634fGtBcMZO3VEfOmh9/XcoWEfLlWWG+g=="",
      ""type"": ""package"",
      ""path"": ""microsoft.codecoverage/17.3.0"",
      ""files"": [
        "".nupkg.metadata"",
        "".signature.p7s"",
        ""Icon.png"",
        ""LICENSE_NET.txt"",
        ""ThirdPartyNotices.txt"",
        ""build/netstandard1.0/CodeCoverage/CodeCoverage.config"",
        ""build/netstandard1.0/CodeCoverage/CodeCoverage.exe"",
        ""build/netstandard1.0/CodeCoverage/VanguardInstrumentationProfiler_x86.config"",
        ""build/netstandard1.0/CodeCoverage/amd64/CodeCoverage.exe"",
        ""build/netstandard1.0/CodeCoverage/amd64/VanguardInstrumentationProfiler_x64.config"",
        ""build/netstandard1.0/CodeCoverage/amd64/covrun64.dll"",
        ""build/netstandard1.0/CodeCoverage/amd64/msdia140.dll"",
        ""build/netstandard1.0/CodeCoverage/arm64/VanguardInstrumentationProfiler_arm64.config"",
        ""build/netstandard1.0/CodeCoverage/arm64/covrunarm64.dll"",
        ""build/netstandard1.0/CodeCoverage/arm64/msdia140.dll"",
        ""build/netstandard1.0/CodeCoverage/codecoveragemessages.dll"",
        ""build/netstandard1.0/CodeCoverage/coreclr/Microsoft.VisualStudio.CodeCoverage.Shim.dll"",
        ""build/netstandard1.0/CodeCoverage/covrun32.dll"",
        ""build/netstandard1.0/CodeCoverage/msdia140.dll"",
        ""build/netstandard1.0/InstrumentationEngine/alpine/x64/VanguardInstrumentationProfiler_x64.config"",
        ""build/netstandard1.0/InstrumentationEngine/alpine/x64/libCoverageInstrumentationMethod.so"",
        ""build/netstandard1.0/InstrumentationEngine/alpine/x64/libInstrumentationEngine.so"",
        ""build/netstandard1.0/InstrumentationEngine/arm64/MicrosoftInstrumentationEngine_arm64.dll"",
        ""build/netstandard1.0/InstrumentationEngine/macos/x64/VanguardInstrumentationProfiler_x64.config"",
        ""build/netstandard1.0/InstrumentationEngine/macos/x64/libCoverageInstrumentationMethod.dylib"",
        ""build/netstandard1.0/InstrumentationEngine/macos/x64/libInstrumentationEngine.dylib"",
        ""build/netstandard1.0/InstrumentationEngine/ubuntu/x64/VanguardInstrumentationProfiler_x64.config"",
        ""build/netstandard1.0/InstrumentationEngine/ubuntu/x64/libCoverageInstrumentationMethod.so"",
        ""build/netstandard1.0/InstrumentationEngine/ubuntu/x64/libInstrumentationEngine.so"",
        ""build/netstandard1.0/InstrumentationEngine/x64/MicrosoftInstrumentationEngine_x64.dll"",
        ""build/netstandard1.0/InstrumentationEngine/x86/MicrosoftInstrumentationEngine_x86.dll"",
        ""build/netstandard1.0/Microsoft.CodeCoverage.Core.dll"",
        ""build/netstandard1.0/Microsoft.CodeCoverage.Instrumentation.dll"",
        ""build/netstandard1.0/Microsoft.CodeCoverage.Interprocess.dll"",
        ""build/netstandard1.0/Microsoft.CodeCoverage.props"",
        ""build/netstandard1.0/Microsoft.CodeCoverage.targets"",
        ""build/netstandard1.0/Microsoft.VisualStudio.TraceDataCollector.dll"",
        ""build/netstandard1.0/Mono.Cecil.Pdb.dll"",
        ""build/netstandard1.0/Mono.Cecil.dll"",
        ""build/netstandard1.0/ThirdPartyNotices.txt"",
        ""build/netstandard1.0/cs/Microsoft.VisualStudio.TraceDataCollector.resources.dll"",
        ""build/netstandard1.0/de/Microsoft.VisualStudio.TraceDataCollector.resources.dll"",
        ""build/netstandard1.0/es/Microsoft.VisualStudio.TraceDataCollector.resources.dll"",
        ""build/netstandard1.0/fr/Microsoft.VisualStudio.TraceDataCollector.resources.dll"",
        ""build/netstandard1.0/it/Microsoft.VisualStudio.TraceDataCollector.resources.dll"",
        ""build/netstandard1.0/ja/Microsoft.VisualStudio.TraceDataCollector.resources.dll"",
        ""build/netstandard1.0/ko/Microsoft.VisualStudio.TraceDataCollector.resources.dll"",
        ""build/netstandard1.0/pl/Microsoft.VisualStudio.TraceDataCollector.resources.dll"",
        ""build/netstandard1.0/pt-BR/Microsoft.VisualStudio.TraceDataCollector.resources.dll"",
        ""build/netstandard1.0/ru/Microsoft.VisualStudio.TraceDataCollector.resources.dll"",
        ""build/netstandard1.0/tr/Microsoft.VisualStudio.TraceDataCollector.resources.dll"",
        ""build/netstandard1.0/zh-Hans/Microsoft.VisualStudio.TraceDataCollector.resources.dll"",
        ""build/netstandard1.0/zh-Hant/Microsoft.VisualStudio.TraceDataCollector.resources.dll"",
        ""lib/net45/Microsoft.VisualStudio.CodeCoverage.Shim.dll"",
        ""lib/netcoreapp1.0/Microsoft.VisualStudio.CodeCoverage.Shim.dll"",
        ""microsoft.codecoverage.17.3.0.nupkg.sha512"",
        ""microsoft.codecoverage.nuspec""
      ]
    },
    ""Microsoft.NET.Test.Sdk/17.3.0"": {
      ""sha512"": ""ch4JCT7AZdBzvCAKD36t6fDsl7NEzzunwW7MwXUG2uFPoWcMd8B8KYg5fiwxnpdXJHodJk6yIBdSwMpN3Ikt9w=="",
      ""type"": ""package"",
      ""path"": ""microsoft.net.test.sdk/17.3.0"",
      ""files"": [
        "".nupkg.metadata"",
        "".signature.p7s"",
        ""Icon.png"",
        ""LICENSE_NET.txt"",
        ""build/net40/Microsoft.NET.Test.Sdk.props"",
        ""build/net40/Microsoft.NET.Test.Sdk.targets"",
        ""build/net45/Microsoft.NET.Test.Sdk.props"",
        ""build/net45/Microsoft.NET.Test.Sdk.targets"",
        ""build/netcoreapp1.0/Microsoft.NET.Test.Sdk.Program.cs"",
        ""build/netcoreapp1.0/Microsoft.NET.Test.Sdk.Program.fs"",
        ""build/netcoreapp1.0/Microsoft.NET.Test.Sdk.Program.vb"",
        ""build/netcoreapp1.0/Microsoft.NET.Test.Sdk.props"",
        ""build/netcoreapp1.0/Microsoft.NET.Test.Sdk.targets"",
        ""build/netcoreapp2.1/Microsoft.NET.Test.Sdk.Program.cs"",
        ""build/netcoreapp2.1/Microsoft.NET.Test.Sdk.Program.fs"",
        ""build/netcoreapp2.1/Microsoft.NET.Test.Sdk.Program.vb"",
        ""build/netcoreapp2.1/Microsoft.NET.Test.Sdk.props"",
        ""build/netcoreapp2.1/Microsoft.NET.Test.Sdk.targets"",
        ""build/uap10.0/Microsoft.NET.Test.Sdk.props"",
        ""buildMultiTargeting/Microsoft.NET.Test.Sdk.props"",
        ""lib/net40/_._"",
        ""lib/net45/_._"",
        ""lib/netcoreapp1.0/_._"",
        ""lib/netcoreapp2.1/_._"",
        ""lib/uap10.0/_._"",
        ""microsoft.net.test.sdk.17.3.0.nupkg.sha512"",
        ""microsoft.net.test.sdk.nuspec""
      ]
    },
    ""Microsoft.TestPlatform.ObjectModel/17.3.0"": {
      ""sha512"": ""6NRzi6QbmWV49Psf8A9z1LTJU4nBrlJdCcDOUyD4Ttm1J2wvksu98GlV+52CkxtpgNsUjGr9Mv1Rbb1/dB06yQ=="",
      ""type"": ""package"",
      ""path"": ""microsoft.testplatform.objectmodel/17.3.0"",
      ""files"": [
        "".nupkg.metadata"",
        "".signature.p7s"",
        ""Icon.png"",
        ""LICENSE_NET.txt"",
        ""lib/net45/Microsoft.TestPlatform.CoreUtilities.dll"",
        ""lib/net45/Microsoft.TestPlatform.PlatformAbstractions.dll"",
        ""lib/net45/Microsoft.VisualStudio.TestPlatform.ObjectModel.dll"",
        ""lib/net45/cs/Microsoft.TestPlatform.CoreUtilities.resources.dll"",
        ""lib/net45/cs/Microsoft.VisualStudio.TestPlatform.ObjectModel.resources.dll"",
        ""lib/net45/de/Microsoft.TestPlatform.CoreUtilities.resources.dll"",
        ""lib/net45/de/Microsoft.VisualStudio.TestPlatform.ObjectModel.resources.dll"",
        ""lib/net45/es/Microsoft.TestPlatform.CoreUtilities.resources.dll"",
        ""lib/net45/es/Microsoft.VisualStudio.TestPlatform.ObjectModel.resources.dll"",
        ""lib/net45/fr/Microsoft.TestPlatform.CoreUtilities.resources.dll"",
        ""lib/net45/fr/Microsoft.VisualStudio.TestPlatform.ObjectModel.resources.dll"",
        ""lib/net45/it/Microsoft.TestPlatform.CoreUtilities.resources.dll"",
        ""lib/net45/it/Microsoft.VisualStudio.TestPlatform.ObjectModel.resources.dll"",
        ""lib/net45/ja/Microsoft.TestPlatform.CoreUtilities.resources.dll"",
        ""lib/net45/ja/Microsoft.VisualStudio.TestPlatform.ObjectModel.resources.dll"",
        ""lib/net45/ko/Microsoft.TestPlatform.CoreUtilities.resources.dll"",
        ""lib/net45/ko/Microsoft.VisualStudio.TestPlatform.ObjectModel.resources.dll"",
        ""lib/net45/pl/Microsoft.TestPlatform.CoreUtilities.resources.dll"",
        ""lib/net45/pl/Microsoft.VisualStudio.TestPlatform.ObjectModel.resources.dll"",
        ""lib/net45/pt-BR/Microsoft.TestPlatform.CoreUtilities.resources.dll"",
        ""lib/net45/pt-BR/Microsoft.VisualStudio.TestPlatform.ObjectModel.resources.dll"",
        ""lib/net45/ru/Microsoft.TestPlatform.CoreUtilities.resources.dll"",
        ""lib/net45/ru/Microsoft.VisualStudio.TestPlatform.ObjectModel.resources.dll"",
        ""lib/net45/tr/Microsoft.TestPlatform.CoreUtilities.resources.dll"",
        ""lib/net45/tr/Microsoft.VisualStudio.TestPlatform.ObjectModel.resources.dll"",
        ""lib/net45/zh-Hans/Microsoft.TestPlatform.CoreUtilities.resources.dll"",
        ""lib/net45/zh-Hans/Microsoft.VisualStudio.TestPlatform.ObjectModel.resources.dll"",
        ""lib/net45/zh-Hant/Microsoft.TestPlatform.CoreUtilities.resources.dll"",
        ""lib/net45/zh-Hant/Microsoft.VisualStudio.TestPlatform.ObjectModel.resources.dll"",
        ""lib/net451/Microsoft.TestPlatform.CoreUtilities.dll"",
        ""lib/net451/Microsoft.TestPlatform.PlatformAbstractions.dll"",
        ""lib/net451/Microsoft.VisualStudio.TestPlatform.ObjectModel.dll"",
        ""lib/net451/cs/Microsoft.TestPlatform.CoreUtilities.resources.dll"",
        ""lib/net451/cs/Microsoft.VisualStudio.TestPlatform.ObjectModel.resources.dll"",
        ""lib/net451/de/Microsoft.TestPlatform.CoreUtilities.resources.dll"",
        ""lib/net451/de/Microsoft.VisualStudio.TestPlatform.ObjectModel.resources.dll"",
        ""lib/net451/es/Microsoft.TestPlatform.CoreUtilities.resources.dll"",
        ""lib/net451/es/Microsoft.VisualStudio.TestPlatform.ObjectModel.resources.dll"",
        ""lib/net451/fr/Microsoft.TestPlatform.CoreUtilities.resources.dll"",
        ""lib/net451/fr/Microsoft.VisualStudio.TestPlatform.ObjectModel.resources.dll"",
        ""lib/net451/it/Microsoft.TestPlatform.CoreUtilities.resources.dll"",
        ""lib/net451/it/Microsoft.VisualStudio.TestPlatform.ObjectModel.resources.dll"",
        ""lib/net451/ja/Microsoft.TestPlatform.CoreUtilities.resources.dll"",
        ""lib/net451/ja/Microsoft.VisualStudio.TestPlatform.ObjectModel.resources.dll"",
        ""lib/net451/ko/Microsoft.TestPlatform.CoreUtilities.resources.dll"",
        ""lib/net451/ko/Microsoft.VisualStudio.TestPlatform.ObjectModel.resources.dll"",
        ""lib/net451/pl/Microsoft.TestPlatform.CoreUtilities.resources.dll"",
        ""lib/net451/pl/Microsoft.VisualStudio.TestPlatform.ObjectModel.resources.dll"",
        ""lib/net451/pt-BR/Microsoft.TestPlatform.CoreUtilities.resources.dll"",
        ""lib/net451/pt-BR/Microsoft.VisualStudio.TestPlatform.ObjectModel.resources.dll"",
        ""lib/net451/ru/Microsoft.TestPlatform.CoreUtilities.resources.dll"",
        ""lib/net451/ru/Microsoft.VisualStudio.TestPlatform.ObjectModel.resources.dll"",
        ""lib/net451/tr/Microsoft.TestPlatform.CoreUtilities.resources.dll"",
        ""lib/net451/tr/Microsoft.VisualStudio.TestPlatform.ObjectModel.resources.dll"",
        ""lib/net451/zh-Hans/Microsoft.TestPlatform.CoreUtilities.resources.dll"",
        ""lib/net451/zh-Hans/Microsoft.VisualStudio.TestPlatform.ObjectModel.resources.dll"",
        ""lib/net451/zh-Hant/Microsoft.TestPlatform.CoreUtilities.resources.dll"",
        ""lib/net451/zh-Hant/Microsoft.VisualStudio.TestPlatform.ObjectModel.resources.dll"",
        ""lib/netcoreapp1.0/Microsoft.TestPlatform.CoreUtilities.dll"",
        ""lib/netcoreapp1.0/Microsoft.TestPlatform.PlatformAbstractions.dll"",
        ""lib/netcoreapp1.0/Microsoft.VisualStudio.TestPlatform.ObjectModel.dll"",
        ""lib/netcoreapp1.0/cs/Microsoft.TestPlatform.CoreUtilities.resources.dll"",
        ""lib/netcoreapp1.0/cs/Microsoft.VisualStudio.TestPlatform.ObjectModel.resources.dll"",
        ""lib/netcoreapp1.0/de/Microsoft.TestPlatform.CoreUtilities.resources.dll"",
        ""lib/netcoreapp1.0/de/Microsoft.VisualStudio.TestPlatform.ObjectModel.resources.dll"",
        ""lib/netcoreapp1.0/es/Microsoft.TestPlatform.CoreUtilities.resources.dll"",
        ""lib/netcoreapp1.0/es/Microsoft.VisualStudio.TestPlatform.ObjectModel.resources.dll"",
        ""lib/netcoreapp1.0/fr/Microsoft.TestPlatform.CoreUtilities.resources.dll"",
        ""lib/netcoreapp1.0/fr/Microsoft.VisualStudio.TestPlatform.ObjectModel.resources.dll"",
        ""lib/netcoreapp1.0/it/Microsoft.TestPlatform.CoreUtilities.resources.dll"",
        ""lib/netcoreapp1.0/it/Microsoft.VisualStudio.TestPlatform.ObjectModel.resources.dll"",
        ""lib/netcoreapp1.0/ja/Microsoft.TestPlatform.CoreUtilities.resources.dll"",
        ""lib/netcoreapp1.0/ja/Microsoft.VisualStudio.TestPlatform.ObjectModel.resources.dll"",
        ""lib/netcoreapp1.0/ko/Microsoft.TestPlatform.CoreUtilities.resources.dll"",
        ""lib/netcoreapp1.0/ko/Microsoft.VisualStudio.TestPlatform.ObjectModel.resources.dll"",
        ""lib/netcoreapp1.0/pl/Microsoft.TestPlatform.CoreUtilities.resources.dll"",
        ""lib/netcoreapp1.0/pl/Microsoft.VisualStudio.TestPlatform.ObjectModel.resources.dll"",
        ""lib/netcoreapp1.0/pt-BR/Microsoft.TestPlatform.CoreUtilities.resources.dll"",
        ""lib/netcoreapp1.0/pt-BR/Microsoft.VisualStudio.TestPlatform.ObjectModel.resources.dll"",
        ""lib/netcoreapp1.0/ru/Microsoft.TestPlatform.CoreUtilities.resources.dll"",
        ""lib/netcoreapp1.0/ru/Microsoft.VisualStudio.TestPlatform.ObjectModel.resources.dll"",
        ""lib/netcoreapp1.0/tr/Microsoft.TestPlatform.CoreUtilities.resources.dll"",
        ""lib/netcoreapp1.0/tr/Microsoft.VisualStudio.TestPlatform.ObjectModel.resources.dll"",
        ""lib/netcoreapp1.0/zh-Hans/Microsoft.TestPlatform.CoreUtilities.resources.dll"",
        ""lib/netcoreapp1.0/zh-Hans/Microsoft.VisualStudio.TestPlatform.ObjectModel.resources.dll"",
        ""lib/netcoreapp1.0/zh-Hant/Microsoft.TestPlatform.CoreUtilities.resources.dll"",
        ""lib/netcoreapp1.0/zh-Hant/Microsoft.VisualStudio.TestPlatform.ObjectModel.resources.dll"",
        ""lib/netcoreapp2.1/Microsoft.TestPlatform.CoreUtilities.dll"",
        ""lib/netcoreapp2.1/Microsoft.TestPlatform.PlatformAbstractions.dll"",
        ""lib/netcoreapp2.1/Microsoft.VisualStudio.TestPlatform.ObjectModel.dll"",
        ""lib/netcoreapp2.1/cs/Microsoft.TestPlatform.CoreUtilities.resources.dll"",
        ""lib/netcoreapp2.1/cs/Microsoft.VisualStudio.TestPlatform.ObjectModel.resources.dll"",
        ""lib/netcoreapp2.1/de/Microsoft.TestPlatform.CoreUtilities.resources.dll"",
        ""lib/netcoreapp2.1/de/Microsoft.VisualStudio.TestPlatform.ObjectModel.resources.dll"",
        ""lib/netcoreapp2.1/es/Microsoft.TestPlatform.CoreUtilities.resources.dll"",
        ""lib/netcoreapp2.1/es/Microsoft.VisualStudio.TestPlatform.ObjectModel.resources.dll"",
        ""lib/netcoreapp2.1/fr/Microsoft.TestPlatform.CoreUtilities.resources.dll"",
        ""lib/netcoreapp2.1/fr/Microsoft.VisualStudio.TestPlatform.ObjectModel.resources.dll"",
        ""lib/netcoreapp2.1/it/Microsoft.TestPlatform.CoreUtilities.resources.dll"",
        ""lib/netcoreapp2.1/it/Microsoft.VisualStudio.TestPlatform.ObjectModel.resources.dll"",
        ""lib/netcoreapp2.1/ja/Microsoft.TestPlatform.CoreUtilities.resources.dll"",
        ""lib/netcoreapp2.1/ja/Microsoft.VisualStudio.TestPlatform.ObjectModel.resources.dll"",
        ""lib/netcoreapp2.1/ko/Microsoft.TestPlatform.CoreUtilities.resources.dll"",
        ""lib/netcoreapp2.1/ko/Microsoft.VisualStudio.TestPlatform.ObjectModel.resources.dll"",
        ""lib/netcoreapp2.1/pl/Microsoft.TestPlatform.CoreUtilities.resources.dll"",
        ""lib/netcoreapp2.1/pl/Microsoft.VisualStudio.TestPlatform.ObjectModel.resources.dll"",
        ""lib/netcoreapp2.1/pt-BR/Microsoft.TestPlatform.CoreUtilities.resources.dll"",
        ""lib/netcoreapp2.1/pt-BR/Microsoft.VisualStudio.TestPlatform.ObjectModel.resources.dll"",
        ""lib/netcoreapp2.1/ru/Microsoft.TestPlatform.CoreUtilities.resources.dll"",
        ""lib/netcoreapp2.1/ru/Microsoft.VisualStudio.TestPlatform.ObjectModel.resources.dll"",
        ""lib/netcoreapp2.1/tr/Microsoft.TestPlatform.CoreUtilities.resources.dll"",
        ""lib/netcoreapp2.1/tr/Microsoft.VisualStudio.TestPlatform.ObjectModel.resources.dll"",
        ""lib/netcoreapp2.1/zh-Hans/Microsoft.TestPlatform.CoreUtilities.resources.dll"",
        ""lib/netcoreapp2.1/zh-Hans/Microsoft.VisualStudio.TestPlatform.ObjectModel.resources.dll"",
        ""lib/netcoreapp2.1/zh-Hant/Microsoft.TestPlatform.CoreUtilities.resources.dll"",
        ""lib/netcoreapp2.1/zh-Hant/Microsoft.VisualStudio.TestPlatform.ObjectModel.resources.dll"",
        ""lib/netstandard1.0/Microsoft.TestPlatform.CoreUtilities.dll"",
        ""lib/netstandard1.0/Microsoft.TestPlatform.PlatformAbstractions.dll"",
        ""lib/netstandard1.0/Microsoft.VisualStudio.TestPlatform.ObjectModel.dll"",
        ""lib/netstandard1.0/cs/Microsoft.TestPlatform.CoreUtilities.resources.dll"",
        ""lib/netstandard1.0/cs/Microsoft.VisualStudio.TestPlatform.ObjectModel.resources.dll"",
        ""lib/netstandard1.0/de/Microsoft.TestPlatform.CoreUtilities.resources.dll"",
        ""lib/netstandard1.0/de/Microsoft.VisualStudio.TestPlatform.ObjectModel.resources.dll"",
        ""lib/netstandard1.0/es/Microsoft.TestPlatform.CoreUtilities.resources.dll"",
        ""lib/netstandard1.0/es/Microsoft.VisualStudio.TestPlatform.ObjectModel.resources.dll"",
        ""lib/netstandard1.0/fr/Microsoft.TestPlatform.CoreUtilities.resources.dll"",
        ""lib/netstandard1.0/fr/Microsoft.VisualStudio.TestPlatform.ObjectModel.resources.dll"",
        ""lib/netstandard1.0/it/Microsoft.TestPlatform.CoreUtilities.resources.dll"",
        ""lib/netstandard1.0/it/Microsoft.VisualStudio.TestPlatform.ObjectModel.resources.dll"",
        ""lib/netstandard1.0/ja/Microsoft.TestPlatform.CoreUtilities.resources.dll"",
        ""lib/netstandard1.0/ja/Microsoft.VisualStudio.TestPlatform.ObjectModel.resources.dll"",
        ""lib/netstandard1.0/ko/Microsoft.TestPlatform.CoreUtilities.resources.dll"",
        ""lib/netstandard1.0/ko/Microsoft.VisualStudio.TestPlatform.ObjectModel.resources.dll"",
        ""lib/netstandard1.0/pl/Microsoft.TestPlatform.CoreUtilities.resources.dll"",
        ""lib/netstandard1.0/pl/Microsoft.VisualStudio.TestPlatform.ObjectModel.resources.dll"",
        ""lib/netstandard1.0/pt-BR/Microsoft.TestPlatform.CoreUtilities.resources.dll"",
        ""lib/netstandard1.0/pt-BR/Microsoft.VisualStudio.TestPlatform.ObjectModel.resources.dll"",
        ""lib/netstandard1.0/ru/Microsoft.TestPlatform.CoreUtilities.resources.dll"",
        ""lib/netstandard1.0/ru/Microsoft.VisualStudio.TestPlatform.ObjectModel.resources.dll"",
        ""lib/netstandard1.0/tr/Microsoft.TestPlatform.CoreUtilities.resources.dll"",
        ""lib/netstandard1.0/tr/Microsoft.VisualStudio.TestPlatform.ObjectModel.resources.dll"",
        ""lib/netstandard1.0/zh-Hans/Microsoft.TestPlatform.CoreUtilities.resources.dll"",
        ""lib/netstandard1.0/zh-Hans/Microsoft.VisualStudio.TestPlatform.ObjectModel.resources.dll"",
        ""lib/netstandard1.0/zh-Hant/Microsoft.TestPlatform.CoreUtilities.resources.dll"",
        ""lib/netstandard1.0/zh-Hant/Microsoft.VisualStudio.TestPlatform.ObjectModel.resources.dll"",
        ""lib/netstandard1.3/Microsoft.TestPlatform.CoreUtilities.dll"",
        ""lib/netstandard1.3/Microsoft.TestPlatform.PlatformAbstractions.dll"",
        ""lib/netstandard1.3/Microsoft.VisualStudio.TestPlatform.ObjectModel.dll"",
        ""lib/netstandard1.3/cs/Microsoft.TestPlatform.CoreUtilities.resources.dll"",
        ""lib/netstandard1.3/cs/Microsoft.VisualStudio.TestPlatform.ObjectModel.resources.dll"",
        ""lib/netstandard1.3/de/Microsoft.TestPlatform.CoreUtilities.resources.dll"",
        ""lib/netstandard1.3/de/Microsoft.VisualStudio.TestPlatform.ObjectModel.resources.dll"",
        ""lib/netstandard1.3/es/Microsoft.TestPlatform.CoreUtilities.resources.dll"",
        ""lib/netstandard1.3/es/Microsoft.VisualStudio.TestPlatform.ObjectModel.resources.dll"",
        ""lib/netstandard1.3/fr/Microsoft.TestPlatform.CoreUtilities.resources.dll"",
        ""lib/netstandard1.3/fr/Microsoft.VisualStudio.TestPlatform.ObjectModel.resources.dll"",
        ""lib/netstandard1.3/it/Microsoft.TestPlatform.CoreUtilities.resources.dll"",
        ""lib/netstandard1.3/it/Microsoft.VisualStudio.TestPlatform.ObjectModel.resources.dll"",
        ""lib/netstandard1.3/ja/Microsoft.TestPlatform.CoreUtilities.resources.dll"",
        ""lib/netstandard1.3/ja/Microsoft.VisualStudio.TestPlatform.ObjectModel.resources.dll"",
        ""lib/netstandard1.3/ko/Microsoft.TestPlatform.CoreUtilities.resources.dll"",
        ""lib/netstandard1.3/ko/Microsoft.VisualStudio.TestPlatform.ObjectModel.resources.dll"",
        ""lib/netstandard1.3/pl/Microsoft.TestPlatform.CoreUtilities.resources.dll"",
        ""lib/netstandard1.3/pl/Microsoft.VisualStudio.TestPlatform.ObjectModel.resources.dll"",
        ""lib/netstandard1.3/pt-BR/Microsoft.TestPlatform.CoreUtilities.resources.dll"",
        ""lib/netstandard1.3/pt-BR/Microsoft.VisualStudio.TestPlatform.ObjectModel.resources.dll"",
        ""lib/netstandard1.3/ru/Microsoft.TestPlatform.CoreUtilities.resources.dll"",
        ""lib/netstandard1.3/ru/Microsoft.VisualStudio.TestPlatform.ObjectModel.resources.dll"",
        ""lib/netstandard1.3/tr/Microsoft.TestPlatform.CoreUtilities.resources.dll"",
        ""lib/netstandard1.3/tr/Microsoft.VisualStudio.TestPlatform.ObjectModel.resources.dll"",
        ""lib/netstandard1.3/zh-Hans/Microsoft.TestPlatform.CoreUtilities.resources.dll"",
        ""lib/netstandard1.3/zh-Hans/Microsoft.VisualStudio.TestPlatform.ObjectModel.resources.dll"",
        ""lib/netstandard1.3/zh-Hant/Microsoft.TestPlatform.CoreUtilities.resources.dll"",
        ""lib/netstandard1.3/zh-Hant/Microsoft.VisualStudio.TestPlatform.ObjectModel.resources.dll"",
        ""lib/netstandard2.0/Microsoft.TestPlatform.CoreUtilities.dll"",
        ""lib/netstandard2.0/Microsoft.TestPlatform.PlatformAbstractions.dll"",
        ""lib/netstandard2.0/Microsoft.VisualStudio.TestPlatform.ObjectModel.dll"",
        ""lib/netstandard2.0/cs/Microsoft.TestPlatform.CoreUtilities.resources.dll"",
        ""lib/netstandard2.0/cs/Microsoft.VisualStudio.TestPlatform.ObjectModel.resources.dll"",
        ""lib/netstandard2.0/de/Microsoft.TestPlatform.CoreUtilities.resources.dll"",
        ""lib/netstandard2.0/de/Microsoft.VisualStudio.TestPlatform.ObjectModel.resources.dll"",
        ""lib/netstandard2.0/es/Microsoft.TestPlatform.CoreUtilities.resources.dll"",
        ""lib/netstandard2.0/es/Microsoft.VisualStudio.TestPlatform.ObjectModel.resources.dll"",
        ""lib/netstandard2.0/fr/Microsoft.TestPlatform.CoreUtilities.resources.dll"",
        ""lib/netstandard2.0/fr/Microsoft.VisualStudio.TestPlatform.ObjectModel.resources.dll"",
        ""lib/netstandard2.0/it/Microsoft.TestPlatform.CoreUtilities.resources.dll"",
        ""lib/netstandard2.0/it/Microsoft.VisualStudio.TestPlatform.ObjectModel.resources.dll"",
        ""lib/netstandard2.0/ja/Microsoft.TestPlatform.CoreUtilities.resources.dll"",
        ""lib/netstandard2.0/ja/Microsoft.VisualStudio.TestPlatform.ObjectModel.resources.dll"",
        ""lib/netstandard2.0/ko/Microsoft.TestPlatform.CoreUtilities.resources.dll"",
        ""lib/netstandard2.0/ko/Microsoft.VisualStudio.TestPlatform.ObjectModel.resources.dll"",
        ""lib/netstandard2.0/pl/Microsoft.TestPlatform.CoreUtilities.resources.dll"",
        ""lib/netstandard2.0/pl/Microsoft.VisualStudio.TestPlatform.ObjectModel.resources.dll"",
        ""lib/netstandard2.0/pt-BR/Microsoft.TestPlatform.CoreUtilities.resources.dll"",
        ""lib/netstandard2.0/pt-BR/Microsoft.VisualStudio.TestPlatform.ObjectModel.resources.dll"",
        ""lib/netstandard2.0/ru/Microsoft.TestPlatform.CoreUtilities.resources.dll"",
        ""lib/netstandard2.0/ru/Microsoft.VisualStudio.TestPlatform.ObjectModel.resources.dll"",
        ""lib/netstandard2.0/tr/Microsoft.TestPlatform.CoreUtilities.resources.dll"",
        ""lib/netstandard2.0/tr/Microsoft.VisualStudio.TestPlatform.ObjectModel.resources.dll"",
        ""lib/netstandard2.0/zh-Hans/Microsoft.TestPlatform.CoreUtilities.resources.dll"",
        ""lib/netstandard2.0/zh-Hans/Microsoft.VisualStudio.TestPlatform.ObjectModel.resources.dll"",
        ""lib/netstandard2.0/zh-Hant/Microsoft.TestPlatform.CoreUtilities.resources.dll"",
        ""lib/netstandard2.0/zh-Hant/Microsoft.VisualStudio.TestPlatform.ObjectModel.resources.dll"",
        ""lib/uap10.0/Microsoft.TestPlatform.CoreUtilities.dll"",
        ""lib/uap10.0/Microsoft.TestPlatform.PlatformAbstractions.dll"",
        ""lib/uap10.0/Microsoft.VisualStudio.TestPlatform.ObjectModel.dll"",
        ""lib/uap10.0/cs/Microsoft.TestPlatform.CoreUtilities.resources.dll"",
        ""lib/uap10.0/cs/Microsoft.VisualStudio.TestPlatform.ObjectModel.resources.dll"",
        ""lib/uap10.0/de/Microsoft.TestPlatform.CoreUtilities.resources.dll"",
        ""lib/uap10.0/de/Microsoft.VisualStudio.TestPlatform.ObjectModel.resources.dll"",
        ""lib/uap10.0/es/Microsoft.TestPlatform.CoreUtilities.resources.dll"",
        ""lib/uap10.0/es/Microsoft.VisualStudio.TestPlatform.ObjectModel.resources.dll"",
        ""lib/uap10.0/fr/Microsoft.TestPlatform.CoreUtilities.resources.dll"",
        ""lib/uap10.0/fr/Microsoft.VisualStudio.TestPlatform.ObjectModel.resources.dll"",
        ""lib/uap10.0/it/Microsoft.TestPlatform.CoreUtilities.resources.dll"",
        ""lib/uap10.0/it/Microsoft.VisualStudio.TestPlatform.ObjectModel.resources.dll"",
        ""lib/uap10.0/ja/Microsoft.TestPlatform.CoreUtilities.resources.dll"",
        ""lib/uap10.0/ja/Microsoft.VisualStudio.TestPlatform.ObjectModel.resources.dll"",
        ""lib/uap10.0/ko/Microsoft.TestPlatform.CoreUtilities.resources.dll"",
        ""lib/uap10.0/ko/Microsoft.VisualStudio.TestPlatform.ObjectModel.resources.dll"",
        ""lib/uap10.0/pl/Microsoft.TestPlatform.CoreUtilities.resources.dll"",
        ""lib/uap10.0/pl/Microsoft.VisualStudio.TestPlatform.ObjectModel.resources.dll"",
        ""lib/uap10.0/pt-BR/Microsoft.TestPlatform.CoreUtilities.resources.dll"",
        ""lib/uap10.0/pt-BR/Microsoft.VisualStudio.TestPlatform.ObjectModel.resources.dll"",
        ""lib/uap10.0/ru/Microsoft.TestPlatform.CoreUtilities.resources.dll"",
        ""lib/uap10.0/ru/Microsoft.VisualStudio.TestPlatform.ObjectModel.resources.dll"",
        ""lib/uap10.0/tr/Microsoft.TestPlatform.CoreUtilities.resources.dll"",
        ""lib/uap10.0/tr/Microsoft.VisualStudio.TestPlatform.ObjectModel.resources.dll"",
        ""lib/uap10.0/zh-Hans/Microsoft.TestPlatform.CoreUtilities.resources.dll"",
        ""lib/uap10.0/zh-Hans/Microsoft.VisualStudio.TestPlatform.ObjectModel.resources.dll"",
        ""lib/uap10.0/zh-Hant/Microsoft.TestPlatform.CoreUtilities.resources.dll"",
        ""lib/uap10.0/zh-Hant/Microsoft.VisualStudio.TestPlatform.ObjectModel.resources.dll"",
        ""microsoft.testplatform.objectmodel.17.3.0.nupkg.sha512"",
        ""microsoft.testplatform.objectmodel.nuspec""
      ]
    },
    ""Microsoft.TestPlatform.TestHost/17.3.0"": {
      ""sha512"": ""uOJALDWtKXZkISKuNI7kOlRi/lk2CqXZtLkNS0Ei+RXqRUjUpCsjAPYSP+DJ/a4QwJ5cI9CVF52vtajnGOaEpw=="",
      ""type"": ""package"",
      ""path"": ""microsoft.testplatform.testhost/17.3.0"",
      ""files"": [
        "".nupkg.metadata"",
        "".signature.p7s"",
        ""Icon.png"",
        ""LICENSE_NET.txt"",
        ""ThirdPartyNotices.txt"",
        ""build/netcoreapp1.0/Microsoft.TestPlatform.TestHost.props"",
        ""build/netcoreapp1.0/x64/testhost.dll"",
        ""build/netcoreapp1.0/x64/testhost.exe"",
        ""build/netcoreapp1.0/x86/testhost.x86.dll"",
        ""build/netcoreapp1.0/x86/testhost.x86.exe"",
        ""build/netcoreapp2.1/Microsoft.TestPlatform.TestHost.props"",
        ""build/netcoreapp2.1/x64/testhost.dll"",
        ""build/netcoreapp2.1/x64/testhost.exe"",
        ""build/netcoreapp2.1/x86/testhost.x86.dll"",
        ""build/netcoreapp2.1/x86/testhost.x86.exe"",
        ""build/uap10.0/Microsoft.TestPlatform.TestHost.props"",
        ""build/uap10.0/Microsoft.TestPlatform.TestHost.targets"",
        ""build/uap10.0/cs/Microsoft.TestPlatform.CommunicationUtilities.resources.dll"",
        ""build/uap10.0/cs/Microsoft.TestPlatform.CoreUtilities.resources.dll"",
        ""build/uap10.0/cs/Microsoft.TestPlatform.CrossPlatEngine.resources.dll"",
        ""build/uap10.0/cs/Microsoft.TestPlatform.Utilities.resources.dll"",
        ""build/uap10.0/cs/Microsoft.VisualStudio.TestPlatform.Common.resources.dll"",
        ""build/uap10.0/cs/Microsoft.VisualStudio.TestPlatform.ObjectModel.resources.dll"",
        ""build/uap10.0/de/Microsoft.TestPlatform.CommunicationUtilities.resources.dll"",
        ""build/uap10.0/de/Microsoft.TestPlatform.CoreUtilities.resources.dll"",
        ""build/uap10.0/de/Microsoft.TestPlatform.CrossPlatEngine.resources.dll"",
        ""build/uap10.0/de/Microsoft.TestPlatform.Utilities.resources.dll"",
        ""build/uap10.0/de/Microsoft.VisualStudio.TestPlatform.Common.resources.dll"",
        ""build/uap10.0/de/Microsoft.VisualStudio.TestPlatform.ObjectModel.resources.dll"",
        ""build/uap10.0/es/Microsoft.TestPlatform.CommunicationUtilities.resources.dll"",
        ""build/uap10.0/es/Microsoft.TestPlatform.CoreUtilities.resources.dll"",
        ""build/uap10.0/es/Microsoft.TestPlatform.CrossPlatEngine.resources.dll"",
        ""build/uap10.0/es/Microsoft.TestPlatform.Utilities.resources.dll"",
        ""build/uap10.0/es/Microsoft.VisualStudio.TestPlatform.Common.resources.dll"",
        ""build/uap10.0/es/Microsoft.VisualStudio.TestPlatform.ObjectModel.resources.dll"",
        ""build/uap10.0/fr/Microsoft.TestPlatform.CommunicationUtilities.resources.dll"",
        ""build/uap10.0/fr/Microsoft.TestPlatform.CoreUtilities.resources.dll"",
        ""build/uap10.0/fr/Microsoft.TestPlatform.CrossPlatEngine.resources.dll"",
        ""build/uap10.0/fr/Microsoft.TestPlatform.Utilities.resources.dll"",
        ""build/uap10.0/fr/Microsoft.VisualStudio.TestPlatform.Common.resources.dll"",
        ""build/uap10.0/fr/Microsoft.VisualStudio.TestPlatform.ObjectModel.resources.dll"",
        ""build/uap10.0/it/Microsoft.TestPlatform.CommunicationUtilities.resources.dll"",
        ""build/uap10.0/it/Microsoft.TestPlatform.CoreUtilities.resources.dll"",
        ""build/uap10.0/it/Microsoft.TestPlatform.CrossPlatEngine.resources.dll"",
        ""build/uap10.0/it/Microsoft.TestPlatform.Utilities.resources.dll"",
        ""build/uap10.0/it/Microsoft.VisualStudio.TestPlatform.Common.resources.dll"",
        ""build/uap10.0/it/Microsoft.VisualStudio.TestPlatform.ObjectModel.resources.dll"",
        ""build/uap10.0/ja/Microsoft.TestPlatform.CommunicationUtilities.resources.dll"",
        ""build/uap10.0/ja/Microsoft.TestPlatform.CoreUtilities.resources.dll"",
        ""build/uap10.0/ja/Microsoft.TestPlatform.CrossPlatEngine.resources.dll"",
        ""build/uap10.0/ja/Microsoft.TestPlatform.Utilities.resources.dll"",
        ""build/uap10.0/ja/Microsoft.VisualStudio.TestPlatform.Common.resources.dll"",
        ""build/uap10.0/ja/Microsoft.VisualStudio.TestPlatform.ObjectModel.resources.dll"",
        ""build/uap10.0/ko/Microsoft.TestPlatform.CommunicationUtilities.resources.dll"",
        ""build/uap10.0/ko/Microsoft.TestPlatform.CoreUtilities.resources.dll"",
        ""build/uap10.0/ko/Microsoft.TestPlatform.CrossPlatEngine.resources.dll"",
        ""build/uap10.0/ko/Microsoft.TestPlatform.Utilities.resources.dll"",
        ""build/uap10.0/ko/Microsoft.VisualStudio.TestPlatform.Common.resources.dll"",
        ""build/uap10.0/ko/Microsoft.VisualStudio.TestPlatform.ObjectModel.resources.dll"",
        ""build/uap10.0/pl/Microsoft.TestPlatform.CommunicationUtilities.resources.dll"",
        ""build/uap10.0/pl/Microsoft.TestPlatform.CoreUtilities.resources.dll"",
        ""build/uap10.0/pl/Microsoft.TestPlatform.CrossPlatEngine.resources.dll"",
        ""build/uap10.0/pl/Microsoft.TestPlatform.Utilities.resources.dll"",
        ""build/uap10.0/pl/Microsoft.VisualStudio.TestPlatform.Common.resources.dll"",
        ""build/uap10.0/pl/Microsoft.VisualStudio.TestPlatform.ObjectModel.resources.dll"",
        ""build/uap10.0/pt-BR/Microsoft.TestPlatform.CommunicationUtilities.resources.dll"",
        ""build/uap10.0/pt-BR/Microsoft.TestPlatform.CoreUtilities.resources.dll"",
        ""build/uap10.0/pt-BR/Microsoft.TestPlatform.CrossPlatEngine.resources.dll"",
        ""build/uap10.0/pt-BR/Microsoft.TestPlatform.Utilities.resources.dll"",
        ""build/uap10.0/pt-BR/Microsoft.VisualStudio.TestPlatform.Common.resources.dll"",
        ""build/uap10.0/pt-BR/Microsoft.VisualStudio.TestPlatform.ObjectModel.resources.dll"",
        ""build/uap10.0/ru/Microsoft.TestPlatform.CommunicationUtilities.resources.dll"",
        ""build/uap10.0/ru/Microsoft.TestPlatform.CoreUtilities.resources.dll"",
        ""build/uap10.0/ru/Microsoft.TestPlatform.CrossPlatEngine.resources.dll"",
        ""build/uap10.0/ru/Microsoft.TestPlatform.Utilities.resources.dll"",
        ""build/uap10.0/ru/Microsoft.VisualStudio.TestPlatform.Common.resources.dll"",
        ""build/uap10.0/ru/Microsoft.VisualStudio.TestPlatform.ObjectModel.resources.dll"",
        ""build/uap10.0/tr/Microsoft.TestPlatform.CommunicationUtilities.resources.dll"",
        ""build/uap10.0/tr/Microsoft.TestPlatform.CoreUtilities.resources.dll"",
        ""build/uap10.0/tr/Microsoft.TestPlatform.CrossPlatEngine.resources.dll"",
        ""build/uap10.0/tr/Microsoft.TestPlatform.Utilities.resources.dll"",
        ""build/uap10.0/tr/Microsoft.VisualStudio.TestPlatform.Common.resources.dll"",
        ""build/uap10.0/tr/Microsoft.VisualStudio.TestPlatform.ObjectModel.resources.dll"",
        ""build/uap10.0/x64/msdia140.dll"",
        ""build/uap10.0/x86/msdia140.dll"",
        ""build/uap10.0/zh-Hans/Microsoft.TestPlatform.CommunicationUtilities.resources.dll"",
        ""build/uap10.0/zh-Hans/Microsoft.TestPlatform.CoreUtilities.resources.dll"",
        ""build/uap10.0/zh-Hans/Microsoft.TestPlatform.CrossPlatEngine.resources.dll"",
        ""build/uap10.0/zh-Hans/Microsoft.TestPlatform.Utilities.resources.dll"",
        ""build/uap10.0/zh-Hans/Microsoft.VisualStudio.TestPlatform.Common.resources.dll"",
        ""build/uap10.0/zh-Hans/Microsoft.VisualStudio.TestPlatform.ObjectModel.resources.dll"",
        ""build/uap10.0/zh-Hant/Microsoft.TestPlatform.CommunicationUtilities.resources.dll"",
        ""build/uap10.0/zh-Hant/Microsoft.TestPlatform.CoreUtilities.resources.dll"",
        ""build/uap10.0/zh-Hant/Microsoft.TestPlatform.CrossPlatEngine.resources.dll"",
        ""build/uap10.0/zh-Hant/Microsoft.TestPlatform.Utilities.resources.dll"",
        ""build/uap10.0/zh-Hant/Microsoft.VisualStudio.TestPlatform.Common.resources.dll"",
        ""build/uap10.0/zh-Hant/Microsoft.VisualStudio.TestPlatform.ObjectModel.resources.dll"",
        ""lib/net45/_._"",
        ""lib/netcoreapp1.0/Microsoft.TestPlatform.CommunicationUtilities.dll"",
        ""lib/netcoreapp1.0/Microsoft.TestPlatform.CoreUtilities.dll"",
        ""lib/netcoreapp1.0/Microsoft.TestPlatform.CrossPlatEngine.dll"",
        ""lib/netcoreapp1.0/Microsoft.TestPlatform.PlatformAbstractions.dll"",
        ""lib/netcoreapp1.0/Microsoft.TestPlatform.Utilities.dll"",
        ""lib/netcoreapp1.0/Microsoft.VisualStudio.TestPlatform.Common.dll"",
        ""lib/netcoreapp1.0/Microsoft.VisualStudio.TestPlatform.ObjectModel.dll"",
        ""lib/netcoreapp1.0/cs/Microsoft.TestPlatform.CommunicationUtilities.resources.dll"",
        ""lib/netcoreapp1.0/cs/Microsoft.TestPlatform.CrossPlatEngine.resources.dll"",
        ""lib/netcoreapp1.0/cs/Microsoft.VisualStudio.TestPlatform.Common.resources.dll"",
        ""lib/netcoreapp1.0/de/Microsoft.TestPlatform.CommunicationUtilities.resources.dll"",
        ""lib/netcoreapp1.0/de/Microsoft.TestPlatform.CrossPlatEngine.resources.dll"",
        ""lib/netcoreapp1.0/de/Microsoft.VisualStudio.TestPlatform.Common.resources.dll"",
        ""lib/netcoreapp1.0/es/Microsoft.TestPlatform.CommunicationUtilities.resources.dll"",
        ""lib/netcoreapp1.0/es/Microsoft.TestPlatform.CrossPlatEngine.resources.dll"",
        ""lib/netcoreapp1.0/es/Microsoft.VisualStudio.TestPlatform.Common.resources.dll"",
        ""lib/netcoreapp1.0/fr/Microsoft.TestPlatform.CommunicationUtilities.resources.dll"",
        ""lib/netcoreapp1.0/fr/Microsoft.TestPlatform.CrossPlatEngine.resources.dll"",
        ""lib/netcoreapp1.0/fr/Microsoft.VisualStudio.TestPlatform.Common.resources.dll"",
        ""lib/netcoreapp1.0/it/Microsoft.TestPlatform.CommunicationUtilities.resources.dll"",
        ""lib/netcoreapp1.0/it/Microsoft.TestPlatform.CrossPlatEngine.resources.dll"",
        ""lib/netcoreapp1.0/it/Microsoft.VisualStudio.TestPlatform.Common.resources.dll"",
        ""lib/netcoreapp1.0/ja/Microsoft.TestPlatform.CommunicationUtilities.resources.dll"",
        ""lib/netcoreapp1.0/ja/Microsoft.TestPlatform.CrossPlatEngine.resources.dll"",
        ""lib/netcoreapp1.0/ja/Microsoft.VisualStudio.TestPlatform.Common.resources.dll"",
        ""lib/netcoreapp1.0/ko/Microsoft.TestPlatform.CommunicationUtilities.resources.dll"",
        ""lib/netcoreapp1.0/ko/Microsoft.TestPlatform.CrossPlatEngine.resources.dll"",
        ""lib/netcoreapp1.0/ko/Microsoft.VisualStudio.TestPlatform.Common.resources.dll"",
        ""lib/netcoreapp1.0/pl/Microsoft.TestPlatform.CommunicationUtilities.resources.dll"",
        ""lib/netcoreapp1.0/pl/Microsoft.TestPlatform.CrossPlatEngine.resources.dll"",
        ""lib/netcoreapp1.0/pl/Microsoft.VisualStudio.TestPlatform.Common.resources.dll"",
        ""lib/netcoreapp1.0/pt-BR/Microsoft.TestPlatform.CommunicationUtilities.resources.dll"",
        ""lib/netcoreapp1.0/pt-BR/Microsoft.TestPlatform.CrossPlatEngine.resources.dll"",
        ""lib/netcoreapp1.0/pt-BR/Microsoft.VisualStudio.TestPlatform.Common.resources.dll"",
        ""lib/netcoreapp1.0/ru/Microsoft.TestPlatform.CommunicationUtilities.resources.dll"",
        ""lib/netcoreapp1.0/ru/Microsoft.TestPlatform.CrossPlatEngine.resources.dll"",
        ""lib/netcoreapp1.0/ru/Microsoft.VisualStudio.TestPlatform.Common.resources.dll"",
        ""lib/netcoreapp1.0/testhost.deps.json"",
        ""lib/netcoreapp1.0/testhost.dll"",
        ""lib/netcoreapp1.0/tr/Microsoft.TestPlatform.CommunicationUtilities.resources.dll"",
        ""lib/netcoreapp1.0/tr/Microsoft.TestPlatform.CrossPlatEngine.resources.dll"",
        ""lib/netcoreapp1.0/tr/Microsoft.VisualStudio.TestPlatform.Common.resources.dll"",
        ""lib/netcoreapp1.0/x64/msdia140.dll"",
        ""lib/netcoreapp1.0/x86/msdia140.dll"",
        ""lib/netcoreapp1.0/zh-Hans/Microsoft.TestPlatform.CommunicationUtilities.resources.dll"",
        ""lib/netcoreapp1.0/zh-Hans/Microsoft.TestPlatform.CrossPlatEngine.resources.dll"",
        ""lib/netcoreapp1.0/zh-Hans/Microsoft.VisualStudio.TestPlatform.Common.resources.dll"",
        ""lib/netcoreapp1.0/zh-Hant/Microsoft.TestPlatform.CommunicationUtilities.resources.dll"",
        ""lib/netcoreapp1.0/zh-Hant/Microsoft.TestPlatform.CrossPlatEngine.resources.dll"",
        ""lib/netcoreapp1.0/zh-Hant/Microsoft.VisualStudio.TestPlatform.Common.resources.dll"",
        ""lib/netcoreapp2.1/Microsoft.TestPlatform.CommunicationUtilities.dll"",
        ""lib/netcoreapp2.1/Microsoft.TestPlatform.CoreUtilities.dll"",
        ""lib/netcoreapp2.1/Microsoft.TestPlatform.CrossPlatEngine.dll"",
        ""lib/netcoreapp2.1/Microsoft.TestPlatform.PlatformAbstractions.dll"",
        ""lib/netcoreapp2.1/Microsoft.TestPlatform.Utilities.dll"",
        ""lib/netcoreapp2.1/Microsoft.VisualStudio.TestPlatform.Common.dll"",
        ""lib/netcoreapp2.1/Microsoft.VisualStudio.TestPlatform.ObjectModel.dll"",
        ""lib/netcoreapp2.1/cs/Microsoft.TestPlatform.CommunicationUtilities.resources.dll"",
        ""lib/netcoreapp2.1/cs/Microsoft.TestPlatform.CrossPlatEngine.resources.dll"",
        ""lib/netcoreapp2.1/cs/Microsoft.VisualStudio.TestPlatform.Common.resources.dll"",
        ""lib/netcoreapp2.1/de/Microsoft.TestPlatform.CommunicationUtilities.resources.dll"",
        ""lib/netcoreapp2.1/de/Microsoft.TestPlatform.CrossPlatEngine.resources.dll"",
        ""lib/netcoreapp2.1/de/Microsoft.VisualStudio.TestPlatform.Common.resources.dll"",
        ""lib/netcoreapp2.1/es/Microsoft.TestPlatform.CommunicationUtilities.resources.dll"",
        ""lib/netcoreapp2.1/es/Microsoft.TestPlatform.CrossPlatEngine.resources.dll"",
        ""lib/netcoreapp2.1/es/Microsoft.VisualStudio.TestPlatform.Common.resources.dll"",
        ""lib/netcoreapp2.1/fr/Microsoft.TestPlatform.CommunicationUtilities.resources.dll"",
        ""lib/netcoreapp2.1/fr/Microsoft.TestPlatform.CrossPlatEngine.resources.dll"",
        ""lib/netcoreapp2.1/fr/Microsoft.VisualStudio.TestPlatform.Common.resources.dll"",
        ""lib/netcoreapp2.1/it/Microsoft.TestPlatform.CommunicationUtilities.resources.dll"",
        ""lib/netcoreapp2.1/it/Microsoft.TestPlatform.CrossPlatEngine.resources.dll"",
        ""lib/netcoreapp2.1/it/Microsoft.VisualStudio.TestPlatform.Common.resources.dll"",
        ""lib/netcoreapp2.1/ja/Microsoft.TestPlatform.CommunicationUtilities.resources.dll"",
        ""lib/netcoreapp2.1/ja/Microsoft.TestPlatform.CrossPlatEngine.resources.dll"",
        ""lib/netcoreapp2.1/ja/Microsoft.VisualStudio.TestPlatform.Common.resources.dll"",
        ""lib/netcoreapp2.1/ko/Microsoft.TestPlatform.CommunicationUtilities.resources.dll"",
        ""lib/netcoreapp2.1/ko/Microsoft.TestPlatform.CrossPlatEngine.resources.dll"",
        ""lib/netcoreapp2.1/ko/Microsoft.VisualStudio.TestPlatform.Common.resources.dll"",
        ""lib/netcoreapp2.1/pl/Microsoft.TestPlatform.CommunicationUtilities.resources.dll"",
        ""lib/netcoreapp2.1/pl/Microsoft.TestPlatform.CrossPlatEngine.resources.dll"",
        ""lib/netcoreapp2.1/pl/Microsoft.VisualStudio.TestPlatform.Common.resources.dll"",
        ""lib/netcoreapp2.1/pt-BR/Microsoft.TestPlatform.CommunicationUtilities.resources.dll"",
        ""lib/netcoreapp2.1/pt-BR/Microsoft.TestPlatform.CrossPlatEngine.resources.dll"",
        ""lib/netcoreapp2.1/pt-BR/Microsoft.VisualStudio.TestPlatform.Common.resources.dll"",
        ""lib/netcoreapp2.1/ru/Microsoft.TestPlatform.CommunicationUtilities.resources.dll"",
        ""lib/netcoreapp2.1/ru/Microsoft.TestPlatform.CrossPlatEngine.resources.dll"",
        ""lib/netcoreapp2.1/ru/Microsoft.VisualStudio.TestPlatform.Common.resources.dll"",
        ""lib/netcoreapp2.1/testhost.deps.json"",
        ""lib/netcoreapp2.1/testhost.dll"",
        ""lib/netcoreapp2.1/tr/Microsoft.TestPlatform.CommunicationUtilities.resources.dll"",
        ""lib/netcoreapp2.1/tr/Microsoft.TestPlatform.CrossPlatEngine.resources.dll"",
        ""lib/netcoreapp2.1/tr/Microsoft.VisualStudio.TestPlatform.Common.resources.dll"",
        ""lib/netcoreapp2.1/x64/msdia140.dll"",
        ""lib/netcoreapp2.1/x86/msdia140.dll"",
        ""lib/netcoreapp2.1/zh-Hans/Microsoft.TestPlatform.CommunicationUtilities.resources.dll"",
        ""lib/netcoreapp2.1/zh-Hans/Microsoft.TestPlatform.CrossPlatEngine.resources.dll"",
        ""lib/netcoreapp2.1/zh-Hans/Microsoft.VisualStudio.TestPlatform.Common.resources.dll"",
        ""lib/netcoreapp2.1/zh-Hant/Microsoft.TestPlatform.CommunicationUtilities.resources.dll"",
        ""lib/netcoreapp2.1/zh-Hant/Microsoft.TestPlatform.CrossPlatEngine.resources.dll"",
        ""lib/netcoreapp2.1/zh-Hant/Microsoft.VisualStudio.TestPlatform.Common.resources.dll"",
        ""lib/uap10.0/Microsoft.TestPlatform.CommunicationUtilities.dll"",
        ""lib/uap10.0/Microsoft.TestPlatform.CoreUtilities.dll"",
        ""lib/uap10.0/Microsoft.TestPlatform.CrossPlatEngine.dll"",
        ""lib/uap10.0/Microsoft.TestPlatform.PlatformAbstractions.dll"",
        ""lib/uap10.0/Microsoft.TestPlatform.Utilities.dll"",
        ""lib/uap10.0/Microsoft.VisualStudio.TestPlatform.Common.dll"",
        ""lib/uap10.0/Microsoft.VisualStudio.TestPlatform.ObjectModel.dll"",
        ""lib/uap10.0/testhost.dll"",
        ""microsoft.testplatform.testhost.17.3.0.nupkg.sha512"",
        ""microsoft.testplatform.testhost.nuspec""
      ]
    },
    ""Newtonsoft.Json/12.0.1"": {
      ""sha512"": ""pBR3wCgYWZGiaZDYP+HHYnalVnPJlpP1q55qvVb+adrDHmFMDc1NAKio61xTwftK3Pw5h7TZJPJEEVMd6ty8rg=="",
      ""type"": ""package"",
      ""path"": ""newtonsoft.json/12.0.1"",
      ""files"": [
        "".nupkg.metadata"",
        "".signature.p7s"",
        ""LICENSE.md"",
        ""lib/net20/Newtonsoft.Json.dll"",
        ""lib/net20/Newtonsoft.Json.pdb"",
        ""lib/net20/Newtonsoft.Json.xml"",
        ""lib/net35/Newtonsoft.Json.dll"",
        ""lib/net35/Newtonsoft.Json.pdb"",
        ""lib/net35/Newtonsoft.Json.xml"",
        ""lib/net40/Newtonsoft.Json.dll"",
        ""lib/net40/Newtonsoft.Json.pdb"",
        ""lib/net40/Newtonsoft.Json.xml"",
        ""lib/net45/Newtonsoft.Json.dll"",
        ""lib/net45/Newtonsoft.Json.pdb"",
        ""lib/net45/Newtonsoft.Json.xml"",
        ""lib/netstandard1.0/Newtonsoft.Json.dll"",
        ""lib/netstandard1.0/Newtonsoft.Json.pdb"",
        ""lib/netstandard1.0/Newtonsoft.Json.xml"",
        ""lib/netstandard1.3/Newtonsoft.Json.dll"",
        ""lib/netstandard1.3/Newtonsoft.Json.pdb"",
        ""lib/netstandard1.3/Newtonsoft.Json.xml"",
        ""lib/netstandard2.0/Newtonsoft.Json.dll"",
        ""lib/netstandard2.0/Newtonsoft.Json.pdb"",
        ""lib/netstandard2.0/Newtonsoft.Json.xml"",
        ""lib/portable-net40+sl5+win8+wp8+wpa81/Newtonsoft.Json.dll"",
        ""lib/portable-net40+sl5+win8+wp8+wpa81/Newtonsoft.Json.pdb"",
        ""lib/portable-net40+sl5+win8+wp8+wpa81/Newtonsoft.Json.xml"",
        ""lib/portable-net45+win8+wp8+wpa81/Newtonsoft.Json.dll"",
        ""lib/portable-net45+win8+wp8+wpa81/Newtonsoft.Json.pdb"",
        ""lib/portable-net45+win8+wp8+wpa81/Newtonsoft.Json.xml"",
        ""newtonsoft.json.12.0.1.nupkg.sha512"",
        ""newtonsoft.json.nuspec""
      ]
    },
    ""Newtonsoft.Json.Bson/1.0.2"": {
      ""sha512"": ""QYFyxhaABwmq3p/21VrZNYvCg3DaEoN/wUuw5nmfAf0X3HLjgupwhkEWdgfb9nvGAUIv3osmZoD3kKl4jxEmYQ=="",
      ""type"": ""package"",
      ""path"": ""newtonsoft.json.bson/1.0.2"",
      ""files"": [
        "".nupkg.metadata"",
        "".signature.p7s"",
        ""LICENSE.md"",
        ""lib/net45/Newtonsoft.Json.Bson.dll"",
        ""lib/net45/Newtonsoft.Json.Bson.pdb"",
        ""lib/net45/Newtonsoft.Json.Bson.xml"",
        ""lib/netstandard1.3/Newtonsoft.Json.Bson.dll"",
        ""lib/netstandard1.3/Newtonsoft.Json.Bson.pdb"",
        ""lib/netstandard1.3/Newtonsoft.Json.Bson.xml"",
        ""lib/netstandard2.0/Newtonsoft.Json.Bson.dll"",
        ""lib/netstandard2.0/Newtonsoft.Json.Bson.pdb"",
        ""lib/netstandard2.0/Newtonsoft.Json.Bson.xml"",
        ""newtonsoft.json.bson.1.0.2.nupkg.sha512"",
        ""newtonsoft.json.bson.nuspec""
      ]
    },
    ""NuGet.Frameworks/6.5.0"": {
      ""sha512"": ""QWINE2x3MbTODsWT1Gh71GaGb5icBz4chS8VYvTgsBnsi8esgN6wtHhydd7fvToWECYGq7T4cgBBDiKD/363fg=="",
      ""type"": ""package"",
      ""path"": ""nuget.frameworks/6.5.0"",
      ""files"": [
        "".nupkg.metadata"",
        "".signature.p7s"",
        ""README.md"",
        ""icon.png"",
        ""lib/net472/NuGet.Frameworks.dll"",
        ""lib/netstandard2.0/NuGet.Frameworks.dll"",
        ""nuget.frameworks.6.5.0.nupkg.sha512"",
        ""nuget.frameworks.nuspec""
      ]
    },
    ""System.Reflection.Metadata/1.6.0"": {
      ""sha512"": ""COC1aiAJjCoA5GBF+QKL2uLqEBew4JsCkQmoHKbN3TlOZKa2fKLz5CpiRQKDz0RsAOEGsVKqOD5bomsXq/4STQ=="",
      ""type"": ""package"",
      ""path"": ""system.reflection.metadata/1.6.0"",
      ""files"": [
        "".nupkg.metadata"",
        "".signature.p7s"",
        ""LICENSE.TXT"",
        ""THIRD-PARTY-NOTICES.TXT"",
        ""lib/netstandard1.1/System.Reflection.Metadata.dll"",
        ""lib/netstandard1.1/System.Reflection.Metadata.xml"",
        ""lib/netstandard2.0/System.Reflection.Metadata.dll"",
        ""lib/netstandard2.0/System.Reflection.Metadata.xml"",
        ""lib/portable-net45+win8/System.Reflection.Metadata.dll"",
        ""lib/portable-net45+win8/System.Reflection.Metadata.xml"",
        ""system.reflection.metadata.1.6.0.nupkg.sha512"",
        ""system.reflection.metadata.nuspec"",
        ""useSharedDesignerContext.txt"",
        ""version.txt""
      ]
    }
  },
  ""projectFileDependencyGroups"": {
    "".NETFramework,Version=v4.7.2"": [
      ""Microsoft.Bcl.HashCode >= 1.1.1"",
      ""Microsoft.NET.Test.Sdk >= 17.3.0"",
      ""Newtonsoft.Json.Bson >= 1.0.2"",
      ""NuGet.Frameworks >= 6.5.0""
    ],
    ""net6.0"": [
      ""Microsoft.Bcl.HashCode >= 1.1.1"",
      ""Microsoft.NET.Test.Sdk >= 17.3.0"",
      ""Newtonsoft.Json.Bson >= 1.0.2"",
      ""NuGet.Frameworks >= 6.5.0""
    ]
  },
  ""packageFolders"": {
    ""D:\\stuff\\Shading\\packages"": {},
    ""C:\\Program Files (x86)\\Microsoft Visual Studio\\Shared\\NuGetPackages"": {}
  },
  ""project"": {
    ""version"": ""1.0.0"",
    ""restore"": {
      ""projectUniqueName"": ""D:\\stuff\\Shading\\SampleLibrary\\SampleLibrary.csproj"",
      ""projectName"": ""SampleLibrary"",
      ""projectPath"": ""D:\\stuff\\Shading\\SampleLibrary\\SampleLibrary.csproj"",
      ""packagesPath"": ""D:\\stuff\\Shading\\packages"",
      ""outputPath"": ""D:\\stuff\\Shading\\SampleLibrary\\obj\\"",
      ""projectStyle"": ""PackageReference"",
      ""crossTargeting"": true,
      ""fallbackFolders"": [
        ""C:\\Program Files (x86)\\Microsoft Visual Studio\\Shared\\NuGetPackages""
      ],
      ""configFilePaths"": [
        ""D:\\stuff\\Shading\\NuGet.Config"",
        ""C:\\Users\\jeffkl\\AppData\\Roaming\\NuGet\\NuGet.Config"",
        ""C:\\Program Files (x86)\\NuGet\\Config\\Microsoft.VisualStudio.FallbackLocation.config"",
        ""C:\\Program Files (x86)\\NuGet\\Config\\Microsoft.VisualStudio.Offline.config""
      ],
      ""originalTargetFrameworks"": [
        ""net472"",
        ""net6.0""
      ],
      ""sources"": {
        ""C:\\Program Files\\dotnet\\library-packs"": {},
        ""D:\\stuff\\Shading\\nupkgs"": {},
        ""https://api.nuget.org/v3/index.json"": {}
      },
      ""frameworks"": {
        ""net6.0"": {
          ""targetAlias"": ""net6.0"",
          ""projectReferences"": {}
        },
        ""net472"": {
          ""targetAlias"": ""net472"",
          ""projectReferences"": {}
        }
      },
      ""warningProperties"": {
        ""warnAsError"": [
          ""NU1605""
        ]
      }
    },
    ""frameworks"": {
      ""net6.0"": {
        ""targetAlias"": ""net6.0"",
        ""dependencies"": {
          ""Microsoft.Bcl.HashCode"": {
            ""target"": ""Package"",
            ""version"": ""[1.1.1, )""
          },
          ""Microsoft.NET.Test.Sdk"": {
            ""target"": ""Package"",
            ""version"": ""[17.3.0, )""
          },
          ""Newtonsoft.Json.Bson"": {
            ""target"": ""Package"",
            ""version"": ""[1.0.2, )""
          },
          ""NuGet.Frameworks"": {
            ""target"": ""Package"",
            ""version"": ""[6.5.0, )""
          }
        },
        ""imports"": [
          ""net461"",
          ""net462"",
          ""net47"",
          ""net471"",
          ""net472"",
          ""net48"",
          ""net481""
        ],
        ""assetTargetFallback"": true,
        ""warn"": true,
        ""downloadDependencies"": [
          {
            ""name"": ""NuGet.Frameworks"",
            ""version"": ""[5.11.0, 5.11.0]""
          }
        ],
        ""frameworkReferences"": {
          ""Microsoft.NETCore.App"": {
            ""privateAssets"": ""all""
          }
        },
        ""runtimeIdentifierGraphPath"": ""C:\\Program Files\\dotnet\\sdk\\8.0.100-rc.2.23502.2\\RuntimeIdentifierGraph.json""
      },
      ""net472"": {
        ""targetAlias"": ""net472"",
        ""dependencies"": {
          ""Microsoft.Bcl.HashCode"": {
            ""target"": ""Package"",
            ""version"": ""[1.1.1, )""
          },
          ""Microsoft.NET.Test.Sdk"": {
            ""target"": ""Package"",
            ""version"": ""[17.3.0, )""
          },
          ""Newtonsoft.Json.Bson"": {
            ""target"": ""Package"",
            ""version"": ""[1.0.2, )""
          },
          ""NuGet.Frameworks"": {
            ""target"": ""Package"",
            ""version"": ""[6.5.0, )""
          }
        },
        ""downloadDependencies"": [
          {
            ""name"": ""NuGet.Frameworks"",
            ""version"": ""[5.11.0, 5.11.0]""
          }
        ],
        ""runtimeIdentifierGraphPath"": ""C:\\Program Files\\dotnet\\sdk\\8.0.100-rc.2.23502.2\\RuntimeIdentifierGraph.json""
      }
    }
  }
}";

            NuGetProjectAssetsFileLoader nuGetProjectAssetsFileLoader = new();

            using Stream stream = new StringStream(projectAssetsFileContents);

            NuGetProjectAssetsFile assetsFile = nuGetProjectAssetsFileLoader.ParseNuGetAssetsFile(Environment.CurrentDirectory, stream);

            assetsFile.Keys.ShouldBe(new[]
            {
                ".NETFramework,Version=v4.7.2",
                "net6.0",
            });

            PackageIdentity packageMicrosoftBclHashCode = new PackageIdentity("Microsoft.Bcl.HashCode", "1.1.1");
            PackageIdentity packageMicrosoftCodeCoverage = new PackageIdentity("Microsoft.CodeCoverage", "17.3.0");
            PackageIdentity packageMicrosoftNETTestSdk = new PackageIdentity("Microsoft.NET.Test.Sdk", "17.3.0");
            PackageIdentity packageMicrosoftTestPlatformObjectModel = new PackageIdentity("Microsoft.TestPlatform.ObjectModel", "17.3.0");
            PackageIdentity packageMicrosoftTestPlatformTestHost = new PackageIdentity("Microsoft.TestPlatform.TestHost", "17.3.0");
            PackageIdentity packageNewtonsoftJson_12_0_1 = new PackageIdentity("Newtonsoft.Json", "12.0.1");
            PackageIdentity packageNewtonsoftJson_9_0_1 = new PackageIdentity("Newtonsoft.Json", "9.0.1");
            PackageIdentity packageNewtonsoftJsonBson = new PackageIdentity("Newtonsoft.Json.Bson", "1.0.2");
            PackageIdentity packageNuGetFrameworks_6_5_0 = new PackageIdentity("NuGet.Frameworks", "6.5.0");
            PackageIdentity packageNuGetFrameworks_5_11_0 = new PackageIdentity("NuGet.Frameworks", "5.11.0");
            PackageIdentity packageSystemReflectionMetadata = new PackageIdentity("System.Reflection.Metadata", "1.6.0");

            NuGetProjectAssetsFileSection net472Section = assetsFile[".NETFramework,Version=v4.7.2"];

            net472Section.Packages.Keys.ShouldBe(new[]
            {
                packageMicrosoftBclHashCode,
                packageMicrosoftCodeCoverage,
                packageMicrosoftNETTestSdk,
                packageNewtonsoftJson_12_0_1,
                packageNewtonsoftJsonBson,
                packageNuGetFrameworks_6_5_0,
            });

            net472Section.Packages[packageMicrosoftCodeCoverage].ShouldBe(new HashSet<PackageIdentity>());

            net472Section.Packages[packageMicrosoftNETTestSdk].ShouldBe(new HashSet<PackageIdentity>
            {
                packageMicrosoftCodeCoverage,
            });

            net472Section.Packages[packageNewtonsoftJson_12_0_1].ShouldBe(new HashSet<PackageIdentity>());

            net472Section.Packages[packageNewtonsoftJsonBson].ShouldBe(new HashSet<PackageIdentity>
            {
                packageNewtonsoftJson_12_0_1,
            });

            NuGetProjectAssetsFileSection net60Section = assetsFile["net6.0"];

            net60Section.Packages.Keys.ShouldBe(new[]
            {
                packageMicrosoftBclHashCode,
                packageMicrosoftCodeCoverage,
                packageMicrosoftNETTestSdk,
                packageMicrosoftTestPlatformObjectModel,
                packageMicrosoftTestPlatformTestHost,
                packageNewtonsoftJson_12_0_1,
                packageNewtonsoftJsonBson,
                packageNuGetFrameworks_6_5_0,
                packageSystemReflectionMetadata,
            });

            net60Section.Packages[packageMicrosoftCodeCoverage].ShouldBe(new HashSet<PackageIdentity>());

            net60Section.Packages[packageMicrosoftNETTestSdk].ShouldBe(new HashSet<PackageIdentity>
            {
                packageMicrosoftCodeCoverage,
                packageMicrosoftTestPlatformTestHost,
                packageMicrosoftTestPlatformObjectModel,
                packageNewtonsoftJson_9_0_1,
                packageNuGetFrameworks_5_11_0,
                packageSystemReflectionMetadata,
            });

            net60Section.Packages[packageMicrosoftTestPlatformObjectModel].ShouldBe(new HashSet<PackageIdentity>
            {
                packageNuGetFrameworks_5_11_0,
                packageSystemReflectionMetadata,
            });

            net60Section.Packages[packageMicrosoftTestPlatformTestHost].ShouldBe(new HashSet<PackageIdentity>
            {
                packageMicrosoftTestPlatformObjectModel,
                packageNewtonsoftJson_9_0_1,
                packageNuGetFrameworks_5_11_0,
                packageSystemReflectionMetadata,
            });

            net60Section.Packages[packageNewtonsoftJson_12_0_1].ShouldBe(new HashSet<PackageIdentity>());

            net60Section.Packages[packageNewtonsoftJsonBson].ShouldBe(new HashSet<PackageIdentity>
            {
                packageNewtonsoftJson_12_0_1,
            });

            net60Section.Packages[packageNuGetFrameworks_6_5_0].ShouldBe(new HashSet<PackageIdentity>());

            net60Section.Packages[packageSystemReflectionMetadata].ShouldBe(new HashSet<PackageIdentity>());
        }
    }
}