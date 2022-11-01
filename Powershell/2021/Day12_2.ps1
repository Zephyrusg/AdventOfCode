$path = "D:\Personal Storage\Bas\Git\AdventofCode\2021"
$data = Get-Content -Path "$($path)\inputDay12"
#$data = Get-Content -Path "$($path)\Exampleday12_1"
#$data = Get-Content -Path "$($path)\Exampleday12_2"
#$data = Get-Content -Path "$($path)\Exampleday12_3"


$global:pathways = New-Object Collections.Generic.List[pscustomobject]
$global:allPaths = New-Object Collections.Generic.List[array]
$currentlist = @()
$global:smallcaves = @()

function Find-Pathways{
    param(
        $node, 
        $currentlist
    )

    $revisted = $false
    $tempList = $currentlist.Clone()
    $tempList += $node
    if($node -eq 'end'){
        $global:allPaths.Add($tempList)
        return
    }

    foreach($smallcave IN $smallcaves){
        if($($templist -match $smallcave).count -eq 2){
            $revisted = $true
        }
    }

    $nexthops = $global:pathways.node.$node.nexthops

    foreach($nexthop in $nexthops){
        $nextnode = $global:pathways.node.$nexthop
        if(!($nextnode.type -eq 'small' -and $currentlist -contains $nexthop -and $revisted -eq $true)){
            Find-Pathways -currentlist $tempList -node $nexthop   
        }
    }


}


$obj = [pscustomobject]@{
    node = @{
        start = @{
            type = "Start"
            nexthops = @() 
        }
    } 
    
}

$pathways.Add($obj)

$obj = [pscustomobject]@{
    node =@{
        'End' =@{
            type = "End"
        nexthops = @()
        }
    }
    
}

$pathways.Add($obj)

foreach($line in $data){
    $splitline = $line.split('-')
    for($i=0;$i-lt 2;$i++){

        if(!($pathways.node.keys -contains $splitline[$i])){
            if($splitline[$i] -cmatch "^[a-z]{1,2}$"){
                $type = "Small"
                $global:smallcaves += $splitline[$i]
            }else{
                $type = "Big"
            }
            $obj = [pscustomobject]@{
                node = @{
                    "$($splitline[$i])" = @{
                        type = $type
                        nexthops = @()
                    }
                }
            }
            $pathways.Add($obj)

        }
    }

    $nexthops = $pathways.nodes.$($splitline[0]).nexthops
    if($nexthops -notmatch $($splitline[1]) -and 
    $($splitline[1])  -ne 'start' -and
    $($splitline[0])  -ne 'end' ){
        $nexthop = $($splitline[1])
        $pathways.node.$($splitline[0]).nexthops += $nexthop
    }

    $nexthops = $pathways.nodes.$($splitline[1]).nexthops
    if($nexthops -notmatch $($splitline[1]) -and 
    $($splitline[0])  -ne 'start' -and
    $($splitline[1])  -ne 'end'){
        $nexthop = $($splitline[0])
        $pathways.node.$($splitline[1]).nexthops += $nexthop
    }

}

$currentlist += 'start'
$nexthops = $global:pathways.node.'start'.nexthops
foreach($nexthop in $nexthops){
    Find-Pathways -node $nexthop -currentlist $currentlist
}

$allPaths.Count