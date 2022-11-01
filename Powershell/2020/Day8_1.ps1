$path = "D:\Personal Storage\Bas\Git\Scripts\AdventCode\2020"
#$data = Get-Content -Path "$($path)\Exampleday8"
$data = Get-Content -Path "$($path)\inputDay8"
$ErrorActionPreference = "stop"

[Collections.Generic.List[pscustomobject]]$instructionlist = @()

foreach($line in $data){

    $parts = $line -split " "

    $obj = [PScustomobject]@{
        
        code     = [string]$parts[0]
        number   = ([int]::parse($parts[1]))
        executed = [bool]$false
    }

    $instructionlist.Add($obj)

}

$argument = 0
$i =0

:loop While($i -lt $instructionlist.Count){

    if($instructionlist[$i].executed){
        break loop
    }
    $code = $($instructionlist[$i].code)
    switch ($code) {
        'acc' {  
            $instructionlist[$i].executed = $true
            $argument += $instructionlist[$i].number
            $i++
            
        }
        'jmp' { 
            $instructionlist[$i].executed = $true 
            $pasti = $i
            $i += [int]$($instructionlist[$i].number)
            Write-Host "From i: $pasti to: $i"
        }
        'nop' {
            $instructionlist[$i].executed = $true
            $i++
        }
    }




}

$argument