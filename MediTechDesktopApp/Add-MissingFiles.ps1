<#
    Adds every *.cs / *.xaml file that sits on disk but isn’t yet listed
    in the <Compile>, <Page> or <ApplicationDefinition> sections of your WPF *.csproj.

    ▸ Run from the folder that contains the .csproj, or pass -ProjectPath / -ProjectDir.
#>

param(
    [string]$ProjectPath = "./MediTechDesktopApp.csproj",
    [string]$ProjectDir  = "."
)

# ──────────────────────────────────────────────────────────────────────────────
Write-Host "🔍  Loading project file $ProjectPath …"
[xml]$proj = Get-Content $ProjectPath

# ──────────────────────────────────────────────────────────────────────────────
# 1) Gather every <Compile Include="…">, <Page Include="…">, and <ApplicationDefinition Include="…">  
#    so we can skip adding files that already appear in any of those sections.
# -------------------------------------------------------------------------------
$existingNodes = $proj.SelectNodes('//Compile | //Page | //ApplicationDefinition')
$existing = @()
foreach ($node in $existingNodes) {
    # Pull out the "Include" attribute value
    $inc = $node.Include
    if ($inc) {
        # Normalize backslashes only
        $existing += $inc.Replace('/', '\')
    }
}

# ──────────────────────────────────────────────────────────────────────────────
# 2) Scan the filesystem for .cs and .xaml files (skip bin/obj folders)
# -------------------------------------------------------------------------------
$projRoot = (Get-Item $ProjectDir).FullName
$files = Get-ChildItem $ProjectDir -Recurse -Include *.cs,*.xaml |
         Where-Object {
             $_.FullName -notmatch '\\bin\\' -and $_.FullName -notmatch '\\obj\\'
         }

# ──────────────────────────────────────────────────────────────────────────────
# 3) Ensure there's at least one <ItemGroup> to append to
# -------------------------------------------------------------------------------
if (-not $proj.Project.ItemGroup) {
    $null = $proj.Project.AppendChild($proj.CreateElement('ItemGroup'))
}
$itemGroup = $proj.Project.ItemGroup[0]

foreach ($file in $files) {
    # Compute the relative path *exactly* as it would appear in the .csproj
    $rel = $file.FullName.Replace($projRoot + "\", '') -replace '/', '\'

    # If that relative path is NOT already in <Compile> OR <Page> OR <ApplicationDefinition>…
    if ($existing -notcontains $rel) {

        if ($file.Extension -eq '.cs') {
            # Add a <Compile Include="…"> for *.cs
            $node = $proj.CreateElement('Compile', $proj.Project.NamespaceURI)
            $node.SetAttribute('Include', $rel)
            $null = $itemGroup.AppendChild($node)
            Write-Host "➕  Added <Compile Include=`"$rel`">"
        }
        else {
            # It's a .xaml – add:
            #   <Page Include="…">
            #       <Generator>MSBuild:Compile</Generator>
            #       <SubType>Designer</SubType>
            #   </Page>
            $node = $proj.CreateElement('Page', $proj.Project.NamespaceURI)
            $node.SetAttribute('Include', $rel)

            $gen = $proj.CreateElement('Generator', $proj.Project.NamespaceURI)
            $gen.InnerText = 'MSBuild:Compile'
            $null = $node.AppendChild($gen)

            $sub = $proj.CreateElement('SubType', $proj.Project.NamespaceURI)
            $sub.InnerText = 'Designer'
            $null = $node.AppendChild($sub)

            $null = $itemGroup.AppendChild($node)
            Write-Host "➕  Added <Page Include=`"$rel`">"
        }
    }
    else {
        # Already exists somewhere; skip it
        Write-Host "✔️   Skipped $rel (already in project)"
    }
}

# ──────────────────────────────────────────────────────────────────────────────
# Save back to the project file
# -------------------------------------------------------------------------------
$proj.Save($ProjectPath)
Write-Host "`n✅  Project file updated successfully."
