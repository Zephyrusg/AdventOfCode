$path = "D:\Personal Storage\Bas\Git\AdventofCode\2020"
#$data = Get-Content -Path "$($path)\Exampleday14_2"
$data = Get-Content -Path "$($path)\inputDay14"

$memoryslots = @{}

$programs = New-Object Collections.Generic.List[array]
$global:possibilities =  New-Object Collections.Generic.List[int64]
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


Function Get-MemslotPossibilties{
    param(
        [int]
        $index,
        [char[]]
        $memInput
    )

    for($index; $index -lt $Meminput.Length;$index++){
        if($memInput[$index] -eq 'X'){
            $memduplicate = $memInput.clone()
            $memInput[$index] = '0'
            $memduplicate[$index] = '1'
            $nextbit = $index + 1
            $(get-MemslotPossibilties -index $nextbit -memInput $memduplicate)
        }
    }
    [string]$binarynumber = (-join $memInput)
    $number = ([convert]::ToUInt64($binarynumber,2))
    $global:possibilities.Add($number)
    
    
}

Function ConvertFrom-Bitmask{
    param(
        [int64]
        $number,
        [string]
        $mask
    )

    [char[]]$mask = $mask.ToCharArray()
    $maskLength = $Mask.Length
    
    [char[]]$binarynumber = (([convert]::ToString($number,2)).padleft($maskLength,'0'))
    
    for($i=0;$i -lt $maskLength;$i++){
        if($mask[$i] -ne '0'){
            $binarynumber[$i] = $mask[$i]
        }
    }
    
    [string]$binarynumber = (-join $binarynumber)
    return $binarynumber   
}

$numberProgram = 0
$numberOfPrograms = $programs.Count
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

    $numberofjobs = $jobs.Count
    $numberjob = 0
    foreach($job in $jobs){
        #[int64]$number = ConvertFrom-Bitmask -mask $mask -number $job.value -type number
        [int64]$number = $job.value
        $memBinary = ConvertFrom-Bitmask -mask $mask -number $job.memslot
        $global:possibilities =  New-Object Collections.Generic.List[int64]
        Get-MemslotPossibilties -memInput $($membinary.toCharArray()) -index 0
        Foreach($memslot in $global:possibilities){
            if($memoryslots.keys -notcontains $memslot){
                $memoryslots.Add($memslot,$number)
            }else{
                $memoryslots[$memslot] = $number
            }

        }
        $numberjob++
        Write-host "Job: $numberjob/$numberofjobs Done. Next one"
    }

    $numberProgram++
    write-host "Program: $numberProgram/$numberOfPrograms Done. Next one. Memslot count: $($memoryslots.count)"
    
}  

[bigint]($memoryslots.Values | Measure-Object -Sum).Sum