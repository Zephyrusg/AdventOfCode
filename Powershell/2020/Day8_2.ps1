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
function Copy-Object($obj){
       
    $ms = New-Object System.IO.MemoryStream
    $bf = New-Object System.Runtime.Serialization.Formatters.Binary.BinaryFormatter
    $bf.Serialize($ms, $obj)
    $ms.Position = 0
    
    #Deep copied data
    $newobj = $bf.Deserialize($ms)
    $ms.Close()
    return $newobj
}
function Confirm-Instructions{
    param(
        $Instructionlist,
        [int] $swapPosition
    )

    $Testlist = Copy-object -obj $Instructionlist

    if($Testlist[$swapPosition].code -eq 'jmp'){
        $Testlist[$swapPosition].code = 'nop'
    }elseif($Testlist[$swapPosition].code -eq 'nop'){
        $Testlist[$swapPosition].code = 'jmp'
    }

    $argument = 0
    $i = 0

    While($i -lt $testList.Count){

        if($testList[$i].executed){ 
            #write-host "Breaks at i: $i with Swapposition: $swapPosition"          
            return 0
        }
        $code = $($testList[$i].code)
        switch ($code) {
            'acc' {  
                $testList[$i].executed = $true
                $argument += $testList[$i].number
                $i++
            }
            'jmp' { 
                $testList[$i].executed = $true 
                $i += [int]$($testList[$i].number)
            }
            'nop' {
                $testList[$i].executed = $true
                $i++
            }
        }
    }
    return $argument
}

$argument = 0

for($i=0;$argument -eq 0;$i++){
    if(($instructionlist[$i].code -eq 'jmp') -or ($instructionlist[$i].code -eq 'nop')){
        $argument = Confirm-Instructions -Instructionlist $instructionlist -swapPosition $i
    }
}
$argument