$path = "D:\Personal Storage\Bas\Git\AdventofCode\2020"
#$data = Get-Content -Path "$($path)\Exampleday14"
$data = Get-Content -Path "$($path)\inputDay14"

$memoryslots = @{}

$programs = New-Object Collections.Generic.List[array]

$program = @($data[0]) 

for($i=1;$i -lt $data.Count;$i++){
    if($data[$i] -match 'mem'){
        $program += $data[$i]
    }else{
        $programs.Add($program)
        $program = @($data[$i]) 
    }
}
$programs.Add($program)

Function ConvertFrom-Bitmask{
    param(
        [int64]
        $number,
        [string]
        $mask
    )

    $firstOne = $mask.IndexOf('1')
    [char[]]$neededPartofMask = ($mask.Substring($firstOne ,$($mask.Length) - $firstOne))
    $maskLength = $neededPartofMask.Length

    [char[]]$binarynumber = (([convert]::ToString($number,2)).padleft($maskLength,'0'))

    for($i=0;$i -lt $maskLength;$i++){
        if($neededPartofMask[$i] -ne 'X'){
            $binarynumber[$i] = $neededPartofMask[$i]
        }
    }
    [string]$binarynumber = (-join $binarynumber)
    $number = ([convert]::ToUInt64($binarynumber,2))

    return $number
}

foreach($program in $programs){
    $mask = $program[0] -replace "Mask = ",""   
    $jobs = @()
    for($i=1;$i -lt $program.Count;$i++){
        $parts = $program[$i] -Split " = ",0,"SimpleMatch"
        $value = [int]$parts[1]
        $memslot = [int]($Parts[0].replace("mem[","")).replace("]","")
        $job = [pscustomobject]@{
            memslot = $memslot
            value = $value 
        }
        $jobs += $job
    }


    foreach($job in $jobs){
        [int64]$number = ConvertFrom-Bitmask -mask $mask -number $job.value
        if($memoryslots.keys -notcontains $job.memslot){
            $memoryslots.Add($job.memslot,$number)
        }else{
            $memoryslots[$job.memslot] = $number
        }
        
    }


}  

($memoryslots.Values | Measure-Object -Sum).Sum