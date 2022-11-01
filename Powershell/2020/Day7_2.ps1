$path = "D:\Personal Storage\Bas\Git\Scripts\AdventCode\2020"
#$data = Get-Content -Path "$($path)\Exampleday7_2"
$data = Get-Content -Path "$($path)\inputDay7_1"
$ErrorActionPreference = "stop"

[Collections.Generic.List[pscustomobject]]$rulelist = @()

foreach($item in $data){

    $parts = $item -split ' contain '
    $inbag = $($parts[1]).Replace('.','') -split ", "

    $rule = [pscustomobject]@{


        bagcolour = $parts[0] -replace ' bags', ''
        contain = @()
    }

    foreach($bag in $inbag){
        $lenth = $bag.Length
        if($bag -ne 'no other bags'){
            $bagcolour = $bag.Substring(2,$lenth-2) -replace " bags| Bag",""
            $quantity = $bag.Substring(0,1)
        }else{
            $bagcolour = 'none'
            $quantity = 0
        }

        $obj = [pscustomobject]@{
            bagcolour = $bagcolour
            quantity = $quantity
        }

        $rule.contain += $obj

    }

    $rulelist.add($rule)
}

function RecursiveFindTotalBags ([string] $bagcolour) {
    $rule = @($rulelist | Where-Object {$_.bagcolour -eq $bagcolour})
    $returnbags = 0
    if ($rule.contain[0].quantity -eq 0) 
    {
        return 1
    }
    foreach($containRule in $($rule.contain)) {
        $returnBags += ([int] $containRule.quantity * (RecursiveFindTotalBags -bagcolour ($containRule.bagcolour)))
    }
    return (($returnbags + 1))
}

$answer = (RecursiveFindTotalBags -bagcolour "Shiny Gold") - 1

$answer