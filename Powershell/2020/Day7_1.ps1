$path = "D:\Personal Storage\Bas\Git\Scripts\AdventCode\2020"
#$data = Get-Content -Path "$($path)\Exampleday7_1"
$data = Get-Content -Path "$($path)\inputDay7_1"


[Collections.Generic.List[pscustomobject]]$rulelist = @()
[Collections.Generic.List[pscustomobject]]$foundcombination = @()

foreach($item in $data){

    $parts = $item -split ' contain '


    $rule = [pscustomobject]@{

        bagcolour = $parts[0] -replace ' bags', ''
        contains = $($parts[1]).Replace('.','')
    }

    $rulelist.add($rule)
}

$possiblecolours = $rulelist | Where-Object -Property contains -Match "shiny gold"| Select-Object -Property bagcolour
$foundcombination += $possiblecolours
while($possiblecolours.Count -ne 0){

   
    foreach($bag in $possiblecolours){
        
        $newoptions += $rulelist | Where-Object -Property contains -Match $bag.bagcolour | Select-Object -Property bagcolour
       
    }

    $foundcombination += $newoptions
    $possiblecolours = $newoptions
    $newoptions = @()
    
}

($foundcombination | Select-Object -Property bagcolour -Unique).count