using namespace System.Numerics
using namespace System.Collections.Generic

$path = "D:\Personal Storage\Bas\Git\AdventofCode\2021"
$data = Get-Content -Path "$($path)\inputDaY19"
#$data = Get-Content -Path "$($path)\Exampleday19"

Class Scanner{

    [List[vector3]]$beaconlist = @()
    [bool]$corrected  = $false
    [vector3]$coordinates
    [string]$name 

    Scanner([string]$name){
        $this.name = $name
    }
    SetCoordinates([vector3]$scannerCoordinates){
        $this.coordinates = $scannerCoordinates
    }
    Translate([vector3]$offset){
        for($i =0; $i -lt $this.beaconlist.count; $i++){
            $this.beaconlist[$i] += $offset 
        }
    }


}

$unknownScanners = new-Object Collections.Generic.List[Scanner]

foreach($line in $data){
    if($line -match "---"){
        $line = $line.Replace("---","") -replace (" ","")
        $scanner = [scanner]::new($line)
    }elseif($line -ne ""){
        $item = $line -split(',')
        $beacon = new-Object sYstem.numerics.vector3 -ArgumentList($item[0],$item[1],$item[2])
        $scanner.beaconlist.Add($beacon)
    }elseif($line -eq ""){
        $unknownScanners.Add($scanner)
    }
}
$unknownScanners.add($scanner)

$unknownScanners[0].corrected = $true
$knownScanner = $unknownScanners[0]
$unknownScanners.RemoveAt(0)
$scannercoordinates = new-Object Collections.Generic.List[vector3]

function Set-Transformation{
    param(
        [int]
        $number,
        [List[Vector3]]
        $vectorlist
    )

    $transformList = new-Object Collections.Generic.List[vector3]
    foreach($vector in $vectorlist){
        switch($number){

            1 { $newVector = [vector3]::new( $vector.X,  $vector.Y,  $vector.Z) }
            2 { $newVector = [vector3]::new( $vector.X, -$vector.Z,  $vector.Y) }
            3 { $newVector = [vector3]::new( $vector.X, -$vector.Y, -$vector.Z) }
            4 { $newVector = [vector3]::new( $vector.X,  $vector.Z, -$vector.Y) }

            5 { $newVector = [vector3]::new( $vector.Y,  $vector.Z,  $vector.X) }
            6 { $newVector = [vector3]::new( $vector.Y, -$vector.X,  $vector.Z)}
            7 { $newVector = [vector3]::new( $vector.Y, -$vector.Z, -$vector.X) }
            8 { $newVector = [vector3]::new( $vector.Y,  $vector.X, -$vector.Z) }

            9 { $newVector = [vector3]::new( $vector.Z,  $vector.X,  $vector.Y) }
            10{ $newVector = [vector3]::new( $vector.Z, -$vector.Y,  $vector.X) }
            11{ $newVector = [vector3]::new( $vector.Z, -$vector.X, -$vector.Y) }
            12{ $newVector = [vector3]::new( $vector.Z,  $vector.Y, -$vector.X) }

            13{ $newVector = [vector3]::new(-$vector.Z, -$vector.Y, -$vector.X) }
            14{ $newVector = [vector3]::new(-$vector.Z,  $vector.X, -$vector.Y) }
            15{ $newVector = [vector3]::new(-$vector.Z,  $vector.Y,  $vector.X) }
            16{ $newVector = [vector3]::new(-$vector.Z, -$vector.X,  $vector.Y) }
            
            17{ $newVector = [vector3]::new(-$vector.Y, -$vector.X, -$vector.Z) }
            18{ $newVector = [vector3]::new(-$vector.Y,  $vector.Z, -$vector.X) }
            19{ $newVector = [vector3]::new(-$vector.Y,  $vector.X,  $vector.Z) }
            20{ $newVector = [vector3]::new(-$vector.Y, -$vector.Z,  $vector.X) }

            21{ $newVector = [vector3]::new(-$vector.X, -$vector.Z, -$vector.Y) }
            22{ $newVector = [vector3]::new(-$vector.X,  $vector.Y, -$vector.Z) }
            23{ $newVector = [vector3]::new(-$vector.X,  $vector.Z,  $vector.Y) }
            24{ $newVector = [vector3]::new(-$vector.X, -$vector.Y,  $vector.Z) }

            #25{ $newVector = [vector3]::new( $vector.Y, -$vector.X, -$vector.Z)}
            
        }
        $transformList.Add($newVector)
    }
    return $transformList
}


:mainloop while($unknownScanners.Count -ne 0){

    foreach($unknownScanner in $unknownScanners){

        for($transformType =1; $transformType -lt 26; $transformType++){
            #write-host "Starting with transform: $transformType / 24"
            $resultVectorList = @{}
            
            $rotatedList = Set-Transformation -vectorlist $unknownScanner.beaconlist -number $transformType
            foreach($beacon in $rotatedList){ 
                for($j=0;$j -lt $knownScanner.beaconlist.Count;$j++){
                    $offset = $knownScanner.beaconlist[$j] - $beacon
                    if($resultVectorList.ContainsKey($offset)){
                        $resultVectorList[$offset]++
                    }
                    else {
                        $resultVectorList.Add($offset,1)
                    }
                    
                        
                }
        
                if([bool]($resultVectorList.GetEnumerator().Where({$_.value -ge 12}))){
                    
                    $scannerOffset = $resultVectorList.GetEnumerator().Where({$_.value -ge 12}).Key
                    write-host "Transformation found type $transformType scannerOffset $scannerOffset name: $($unknownscanner.name)"
                    $scannercoordinates.add($scannerOffset)
                    $unknownScanner.beaconlist = $rotatedList
                    $unknownScanner.Translate($scannerOffset)
                    $unknownScanner.corrected = $true
                    foreach($beacon in $unknownScanner.beaconlist){
                        $knownScanner.beaconlist.Add($beacon)
                    }
                    $knownScanner.beaconlist = ([System.Linq.Enumerable]::Union($knownScanner.beaconlist, $unknownScanner.beaconlist)).ToList()
                    $null = $unknownScanners.Remove($unknownScanner)

                    continue mainloop

                }
            }
        }   
    }
}

write-host "BeaconCount: " + $knownScanner.beaconlist.Count
  
$highest = 0

for ($i = 0; $i -lt $scannerCoordinates.Count; $i++){
    for ($j = $i; $j -lt $scannerCoordinates.Count; $j++){
        $X = [Math]::Abs($scannercoordinates[$i].X - $scannercoordinates[$j].X)
        $Y = [Math]::Abs($scannercoordinates[$i].Y - $scannercoordinates[$j].Y)
        $Z = [Math]::Abs($scannercoordinates[$i].Z - $scannercoordinates[$j].Z)
        
        $score = $x + $y + $Z
        if($score -gt $highest){
            $highest = $score
        }
    }
}

Write-Host "Highest Manhatten Distance: " + $highest