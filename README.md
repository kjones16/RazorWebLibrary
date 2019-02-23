# RazorWebLibrary
Razor Class Library with Static File and Typescript support.

I did change TargetFramework to netcoreapp2.2 but may not be needed. 

For TypeScript support import both the Microsoft.Typescript.target and the Microsoft.Typescript.DotNetCore.target into the csproj as shown
below. Make sure to also include the props at the end on the file.

I order to access the js files created by typescript from the referenced web site do the following...

1. Set the typescript output to a wwwroot folder in your Razor Class Library (RCL).
2. Embed the contains of the wwwroot folder. See below.
3. Add references to Microsoft.AspNetCore.StaticFiles, Microsoft.Extensions.FileProviders.Embedded, 
   and Microsoft.Extensions.FileProviders.Physical. You will need the Physical during development so you didn't have to restart the 
   web site after making changes to the TypeScript or cshtml view files.
4. Implement ```IPostConfigureOptions<StaticFileOptions>``` which allows aspnet to resolve the embedded files. The sample code 
   also looks at the physical files during development.
5. Implement ```IPostConfigureOptions<RazorViewEngineOptions>``` for development support.

RazorWebLibrary.csproj
```
<Project Sdk="Microsoft.NET.Sdk.Razor">

  <PropertyGroup>
    <TargetFramework>netcoreapp2.2</TargetFramework>
    <TypeScriptToolsVersion>3.1</TypeScriptToolsVersion>
    <GenerateEmbeddedFilesManifest>true</GenerateEmbeddedFilesManifest>
  </PropertyGroup>

  <!-- Must come after setting TypeScriptToolsVersion (See above) -->
  <Import Project="$(MSBuildExtensionsPath)\Microsoft\VisualStudio\v$(VisualStudioVersion)\TypeScript\Microsoft.TypeScript.targets"
          Condition="Exists('$(MSBuildExtensionsPath)\Microsoft\VisualStudio\v$(VisualStudioVersion)\TypeScript\Microsoft.TypeScript.targets') AND '$(EnableTypeScriptNuGetTarget)' != 'true'"/>

  <Import Project="$(MSBuildExtensionsPath)\Microsoft\VisualStudio\v$(VisualStudioVersion)\TypeScript\Microsoft.TypeScript.DotNetCore.targets"
          Condition="Exists('$(MSBuildExtensionsPath)\Microsoft\VisualStudio\v$(VisualStudioVersion)\TypeScript\Microsoft.TypeScript.DotNetCore.targets') AND '$(EnableTypeScriptNuGetTarget)' != 'true'"/>

  <ItemGroup>
    <PackageReference Include="Microsoft.AspNetCore.Mvc" Version="2.2.0" />
    <PackageReference Include="Microsoft.AspNetCore.StaticFiles" Version="2.2.0" />
    <PackageReference Include="Microsoft.Extensions.FileProviders.Embedded" Version="2.2.0" />
    <PackageReference Include="Microsoft.Extensions.FileProviders.Physical" Version="2.2.0" />
  </ItemGroup>

  <!-- Embed the contains of the wwwroot folder.
  <ItemGroup>
    <EmbeddedResource Include="wwwroot\**\*" />
  </ItemGroup>

  <ItemGroup>
    <None Remove="Scripts\MyFeature.ts" />
  </ItemGroup>
 
  <ItemGroup>
    <Folder Include="wwwroot\" />
    <Folder Include="wwwroot\js\" />
  </ItemGroup>
 
  <ItemGroup>
    <TypeScriptCompile Include="Scripts\MyFeature.ts" />
  </ItemGroup>

  <!-- Don't forget to add the typescript props -->
  <Import Project="$(MSBuildExtensionsPath)\Microsoft\VisualStudio\v$(VisualStudioVersion)\TypeScript\Microsoft.TypeScript.Default.props"
    Condition="Exists('$(MSBuildExtensionsPath)\Microsoft\VisualStudio\v$(VisualStudioVersion)\TypeScript\Microsoft.TypeScript.Default.props')"/>

</Project>
```
