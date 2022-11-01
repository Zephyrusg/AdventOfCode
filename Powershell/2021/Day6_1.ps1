$path = "D:\Personal Storage\Bas\Git\Scripts\AdventCode\2021"
#$data = Get-Content -Path "$($path)\Exampleday6_1" -Raw
$data = Get-Content -Path "$($path)\inputDay6_1"

[Collections.Generic.List[Int]]$initialfishes = ""
#$fishes = @()

$spitString = $data -split ","

foreach($number in $spitString){
   $initialfishes += ([int]::parse($number))
}

[int64[]]$fishes = @(0) * 9
for($i=1;$i -lt $fishes.Count;$i++){
    $fishes[$i] = ($initialfishes.Where({$_ -eq $i})).count
    #write-host "i: $i and initial: $(($initialfishes.Where({$_ -eq $i})).count)"
}

$days = 0
$endDays = 255

while($days -le $endDays){
    $zerofishes = $fishes[0]
    ($Fishes -join ",")  
    for($x=1;$x -lt $fishes.count; $x++){
      $fishes[$x-1] = $fishes[$x]
    }
    $fishes[6] += $zerofishes
    $fishes[8] = $zerofishes
    $days++
    write-host "Days: $days"
}

($fishes | Measure-Object -Sum).sum


