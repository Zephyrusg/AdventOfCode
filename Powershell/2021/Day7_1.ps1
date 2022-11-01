$path = "D:\Personal Storage\Bas\Git\Scripts\AdventCode\2021"
#$data = Get-Content -Path "$($path)\Exampleday7" 
$data = Get-Content -Path "$($path)\inputDay7"

$allcrabs = New-Object Collections.Generic.List[Int]

$spitString = $data -split ","

foreach($number in $spitString){
   $allcrabs.Add(([int]::parse($number)))
}

$maxfuel = ($allcrabs | Measure-Object -Maximum).Maximum
$optimalfuel = 999999


For($i=0;$i -le $maxfuel; $i++){
    
    $Totalfuelneeded = 0
    foreach($crab in $allcrabs){
        
        $fuelneeded = [Math]::Abs($crab - $i)
        $Totalfuelneeded += $fuelneeded
    }
    Write-Host "i: $i totalfuel $totalfuelneeded"
    if($totalfuelneeded -lt $optimalfuel){
        $optimalfuel = $totalfuelneeded
        
    }
}

$optimalfuel