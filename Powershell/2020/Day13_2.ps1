$path = "D:\Personal Storage\Bas\Git\AdventofCode\2020"
#$data = Get-Content -Path "$($path)\Exampleday13_2"
$data = Get-Content -Path "$($path)\inputDay13"
$nl = [System.Environment]::NewLine
$items = $data.split("$nl")[1]
$items = $items.split(',')


$busdata = New-Object Collections.Generic.List[array]

for($i=0;$i -lt $items.count;$i++){
    if($items[$i] -match "[0-9]"){
        $bus = @([int]$items[$i],$i)
        $busdata.Add($bus)
    }
}

[bigint]$stepcount = $busdata[0][0]
[bigint]$step = 0
$nextbusid = $busdata[1][0]
$offset = $busdata[1][1]
$done = 1
write-host "Starting with Bus $($busdata[0][0])"
while($done -lt $($busdata.Count)){
   
    if((($step + $offset) % $nextbusid) -eq 0 ){
        Write-host "Step: $step Stepcount: $stepcount Bus: $nextbusid Offset: $offset"
        $stepcount = 1 
        for($i=0;$i -le $done;$i++){
            $stepcount *= $busdata[$i][0]
        }
        $done++
        if($done -lt $busdata.Count){
            $nextbusid = $busdata[$done][0]
            $offset = $busdata[$done][1]
        }
        
    }
    $step = $step + $stepcount
}