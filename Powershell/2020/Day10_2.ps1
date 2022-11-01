$path = "D:\Personal Storage\Bas\Git\adventofcode\2020"
#[int[]]$data = Get-Content -Path "$($path)\Exampleday10"
[int[]]$data = Get-Content -Path "$($path)\inputDay10"

$data += 0
$data += ($data | Measure-Object -Maximum).Maximum +3

[int[]]$sortedData = $data | Sort-Object 

# Initially, we only know how many paths there are to the starting node
$countFromNode = @{0 = 1}

$numberExists = ,$false * ($sortedData[-1]+4)
$sortedData.ForEach{$numberExists[$_] = $true}

Function Get-PathCountFromNode {
    Param(
        [Parameter(Mandatory, ValueFromPipeline)]
        [int]
        $number
    )
        if (($NodeCount = $CountFromNode[$number]) -gt 0) {
            $NodeCount
        }
        else {
            $PriorNumbers= (($number-1)..($number-3)).Where{$numberExists[$_]}
            Foreach($PriorNumber in $PriorNumbers){
                $PriorPathCounts += $PriorNumber | Get-PathCountFromNode
            }
            $NodeCount = $PriorPathCounts | Measure-Object -Sum | ForEach-Object Sum
            ($CountFromNode[$number] = $NodeCount)
        }
}
$sortedData[-1] | Get-MemoizedPathCountFromJolt



<#function Find-Combination{
    param(
        [ValidateSet(0,1,2,3)] 
        [int]
        $jolt,
        [int]
        $lastnumber,
        [int[]]
        $sortedData
    )

    $possibleCombinations = 0

    #check if number exist
    if($sortedData.IndexOf($($lastnumber+$jolt)) -eq -1){
        return 0
    }else{
        $number = $jolt + $lastnumber
    }
    #check if last number
    if((( $sortedData| Measure-Object -Maximum).Maximum) -eq $number){
        return 1
    }
    #Write-Host "number: $number"
    #search option
    If(($sortedData.IndexOf($($number+1)) -ne -1)){
        $possibleCombinations += Find-Combination -jolt 1 -lastnumber $number -sortedData $sortedData
    }
    if($sortedData.IndexOf($($number+2)) -ne -1){
        $possibleCombinations += Find-Combination -jolt 2 -lastnumber $number -sortedData $sortedData
    }if($sortedData.IndexOf($($number+3)) -ne -1){
        $possibleCombinations += Find-Combination -jolt 3 -lastnumber $number -sortedData $sortedData
    }


    return $possibleCombinations

}


$answer = Find-Combination -jolt 0 -lastnumber 0 -sortedData $sortedData

$answer#>