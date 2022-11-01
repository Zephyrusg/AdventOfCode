$path = "D:\Personal Storage\Bas\Git\Scripts\AdventCode\2021"
#$data = Get-Content -Path "$($path)\Exampleday7" 
$data = Get-Content -Path "$($path)\inputDay7"

$allcrabs = New-Object Collections.Generic.List[Int]

$spitString = $data -split ","

foreach($number in $spitString){
   $allcrabs.Add(([int]::parse($number)))
}

$maxfuel = ($allcrabs | Measure-Object -Maximum).Maximum
$optimalfuel = [int32]::MaxValue


For($i=0;$i -le $maxfuel; $i++){
    
    $Totalfuelneeded = 0
    foreach($crab in $allcrabs){
        
        $distance = [Math]::Abs($crab - $i)
        $fuelneeded = ($distance + 1) * $distance /2
        $Totalfuelneeded += $fuelneeded
    }
    #Write-Host "i: $i totalfuel $totalfuelneeded"
    if($totalfuelneeded -lt $optimalfuel){
        $optimalfuel = $totalfuelneeded
        
    }
}

$optimalfuel