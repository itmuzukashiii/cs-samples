Param (
    [string] $jsonPath
)

Write-Host
Write-Host ('Input File: "{0}":' -f $jsonPath)
Write-Host
$json = Get-Content -Raw -Path $jsonPath
$json

Write-Host
Write-Host 'Convert to PowerShell Object:'
$jsonObj = ConvertFrom-Json $json
$jsonObj

<#
Input File: ".\Students.json":

[
    {
        "Name": "Sakamoto",
        "ID": "A002",
        "Age": 12
    },
    {
        "Name": "John",
        "ID": "B013",
        "Age": 15
    },
    {
        "Name": "Lucy",
        "ID": "C024",
        "Age": 13
    }
]

Convert to PowerShell Object:

Name     ID   Age
----     --   ---
Sakamoto A002  12
John     B013  15
Lucy     C024  13
#>