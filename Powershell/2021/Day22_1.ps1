using namespace System.Numerics
using namespace System.Collections.Generic

$path = "D:\Personal Storage\Bas\Git\AdventofCode\2021"
$data = Get-Content -Path "$($path)\inputDay22"
#$data = Get-Content -Path "$($path)\Exampleday19"

$turnedonPoints = [HashSet[Vector3]]::new()
$linecounter = 1
foreach($line in $data[0..19]){
    write-host "Starting with $linecounter / 20"
    $Line -match '-?\d{1,2}..-?\d{1,2}'

    [bool]$turnon = $Line -match "on"
    $line = $line -replace 'on ',"" -replace 'off ',""
    $parts = $line -split ','

    $null = $parts[0] -match '-?\d{1,2}..-?\d{1,2}'
    $Xpart = $Matches[0]
    $Xpart = $Xpart -split "\.."
    $xMin = [int]::Parse($Xpart[0])
    $xMax = [int]::Parse($Xpart[1])

    $null = $parts[1] -match '-?\d{1,2}..-?\d{1,2}'
    $Ypart = $Matches[0]
    $Ypart = $Ypart -split "\.."
    $yMin = [int]::Parse($Ypart[0])
    $yMax = [int]::Parse($Ypart[1])

    $null = $parts[2] -match '-?\d{1,2}..-?\d{1,2}'
    $zpart = $Matches[0]
    $Zpart = $Zpart -split "\.."
    $zMin = [int]::Parse($Zpart[0])
    $zMax = [int]::Parse($Zpart[1])

    for($x=$xMin; $x -le $xMax; $x++){
        for($y=$yMin; $y -le $yMax; $y++){
            for($z=$zMin; $z -le $zMax; $z++){
                $point = [Vector3]::new($x,$y,$z)
                
                if($turnon){
                    if(!$turnedonPoints.Contains($point)){
                        $null = $turnedonPoints.Add($point)
                    }
                }else{
                    if($turnedonPoints.Contains($point)){
                        $null = $turnedonPoints.Remove($point)
                    }
                }
            }
        }
    }
 $linecounter ++
}

$turnedonPoints.Count