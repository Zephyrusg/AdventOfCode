$path = "D:\Personal Storage\Bas\Git\AdventofCode\2020"
#$data = Get-Content -Path "$($path)\Exampleday13"
$data = Get-Content -Path "$($path)\inputDay13"
$nl = [System.Environment]::NewLine
$time = [int]$data.split("$nl")[0]
$busids = $data.split("$nl")[1]

$busids = $busids.split(',') | Where-Object {$_ -notlike 'x'}

$combinations = New-Object Collections.Generic.List[PSCustomObject]

foreach($busid in $busids){
    $rest = $time % $busid
    $untilnextbus = $busid - $rest

    $obj = [pscustomobject]@{
        "busid"        = $busid
        "untilnextbus" = $untilnextbus
    }
    $combinations.Add($obj)
}

$bestcombi = ($combinations | Sort-Object -Property untilnextbus)[0]

[int]($bestcombi.busid) * [int]($bestcombi.untilnextbus)