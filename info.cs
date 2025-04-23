using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpf_Reservoom3
{
    class Info
    {
        // // Playlist => https://www.youtube.com/watch?v=fZxZswmC_BY&list=PLA8ZIAm2I03hS41Fy4vFpRw8AdYNBXmNm
        // SourceCode => https://github.com/SingletonSean/reservoom
        // figma.com => dokumente erstellen wie Visio?
        // Nugets ::
        //  Microsoft.EntityFrameworkCore
        // Microsoft.EntityFrameworkCore.Sqlite
        // Microsoft.EntitiyFrameworkCore.Tools
        // open package manager in VS : navigate to Tools > NuGet Package Manager > Package Manager Console from the top menu
        // in the PM-console: add-migration Initial and update-database, you can disable the content of ReservoomDesignTimeDbContextFactory.cs file

        // https://www.youtube.com/watch?v=ZQadPrZ7E_A&list=PLA8ZIAm2I03hS41Fy4vFpRw8AdYNBXmNm&index=7

        // Hosting: Micrsoft.Extensions.Hosting => Dependency Injection?
        // https://www.youtube.com/watch?v=dgJ1nS2CLpQ&list=PLA8ZIAm2I03hS41Fy4vFpRw8AdYNBXmNm&index=9

        // Puplishing
        // https://www.youtube.com/watch?v=I_Lj2_IkmtA&list=PLA8ZIAm2I03hS41Fy4vFpRw8AdYNBXmNm&index=10
        // open Terminal => select "Project solution(mappe) => "In Terminal öffnen"
        // PS ...dotnet publish -c Release
        // C:\Users\fd_41\source\repos\Wpf_Reservoom_3\bin\Release\net8.0-windows\Publish

        // https://learn.microsoft.com/en-us/dotnet/core/rid-catalog
        // <RuntimeIdentifier>win-x64</RuntimeIdentifier> in Wpf_Reservoom3 project file
        // PS ...dotnet publish -c Release --self-contained
        // User does not need store .NET Runtime dll's anylonger
        // C:\Users\fd_41\source\repos\Wpf_Reservoom_3\bin\Release\net8.0-windows\win-x64\publish

        // As a single file (well, there are than one file, but fewer than the latest line above):
        // PS ...dotnet publish -c Release --self-contained -p:PublishSingleFile=true
        // C:\Users\fd_41\source\repos\Wpf_Reservoom_3\bin\Release\net8.0-windows\win-x64\publish

        // https://sqlitebrowser.org/dl/
        // https://sqlitebrowser.org/

        // Publishing/Deploying WPF Applications (feat. GitHub Actions) - EASY WPF (.NET Core)
        // https://www.youtube.com/watch?v=VIlDni8-iWM

        // Deploy Nutget Packages
        // https://www.youtube.com/watch?v=cUrrdAVmo4I
        // https://github.com/SingletonSean/wpf-tutorials/blob/master/.github/workflows/deploy-class-commands.yml

        // Wechsle von Projektansicht zu Ordneransicht
        // erstelle ein Ordner namens .github
        // darunter erstelle ein Ordner namens workflows
        // erstelle eine Datei namens deploy-class-commands.yml

        // open Terminal => select "Project solution(mappe) => "In Terminal öffnen"
        // PS...dotnet publish -c Release --self-contained -p:PublishSingleFile=true
        // dotnet publish -c Release --self-contained -r win-x64 -p:PublishSingleFile=true

        // Terminal öffnen 
        // git tag class-commands/v1.0.11-beta
        // git push origin class-commands/v1.0.11-beta
        // https://github.com/madeUthink/Wpf_Reservoom3/actions/workflows/main.yml
        // https://github.com/madeUthink/Wpf_Reservoom3/actions/runs/14603618591/job/40967492721

        // git add .
        // git commit -m "Add beta-release"
        // git tag class-commands/v1.0.0
        // git push origin class-commands/v1.0.0

        // with issues, then...
        // git add .
        // git commit -m "Fix issue"
        // git tag class-commands/v1.0.0
        // git push origin class-commands/v1.0.0



    }
}
