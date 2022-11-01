$location = "D:\Personal Storage\Bas\Git\Scripts\AdventCode\2021"
$data = Get-Content -Path "$location\InputDay3_1"

function Get-LifeSupportRating {
    Param(
    [Parameter(Mandatory)]
    [ValidateSet("oxygen generator", "CO2 scrubber")]
    [string]
    $kind
    )
    if($kind -eq "oxygen generator"){
        $common = "most"
    }else{
        $common = "least"
    }
    

    [int]$lengthbiniarnumber = $data[0].Length
    $columnCounter = 0
    $RemaingPartofData = $data
    for($i=0;$i -lt $lengthbiniarnumber; $i++){
        $listones = @()
        $listzeros = @()

        foreach($row in $RemaingPartofData){
            
            
            if($row[$columnCounter] -eq "0"){
                $listzeros += $row      

            }else{
                $listones += $row 
            }
        }
        switch($common){
            
        
            "least"{   
                    if(($($listzeros.count) -lt $($listones.count)) -or ($($listzeros.count) -eq $($listones.count))){
                        $RemaingPartofData = $listzeros
                    }else{
                        $RemaingPartofData = $listones
                    }
                }
            "most"{
                if(($($listones.count) -gt $($listzeros.count)) -or ($($listzeros.count) -eq $($listones.count))){
                    $RemaingPartofData = $listones
                }else{
                    $RemaingPartofData = $listzeros
                }
            }
        
        }
        if($RemaingPartofData.count -eq 1){
            return [convert]::Toint32($RemaingPartofData,2)
        }
        $columnCounter++
    }
}


$oxyen = get-LifeSupportRating -kind 'oxygen generator'
$co2 = get-LifeSupportRating -kind "CO2 scrubber"

$anwser = $co2 * $oxyen