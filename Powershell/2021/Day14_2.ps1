$path = "D:\Personal Storage\Bas\Git\AdventofCode\2021"
$data = Get-Content -Path "$($path)\inputDay14"
#$data = Get-Content -Path "$($path)\Exampleday14"

$times = 40

[string]$startstring =  $data[0..1]

if($startstring[$($startstring.Length -1)] -eq " "){
    $startstring = $startstring.Remove($($startstring.Length -1),1)
}

$keys =  $data[2..$($data.Count-1)]

$countpolymerpairs  = @{}
$polymerTemplates = @{}
$endcountletter = @{}
[long]$count = 0

foreach($template in $keys){
    $polymerTemplates.Add($template.Substring(0,2),$template[-1])
    $countpolymerpairs.Add($template.Substring(0,2),$count)
    if($endcountletter.keys -notcontains $template[-1]){
        $endcountletter.Add([string]$template[-1],0)
    } 
}

#intial set
for($i=1;$i -lt $startstring.Length;$i++ ){
    $pair = $($startstring[$i-1] + $startstring[$i])
    $countpolymerpairs[$pair] ++
}

for($i=0; $i -lt $times;$i++){

    
    $newstate = $countpolymerpairs.clone()
    foreach($pair in $countpolymerpairs.keys){
        if($countpolymerpairs[$pair] -gt 0){
  
            $quantity = $countpolymerpairs[$pair] 
            $extra = $polymerTemplates[$pair]
            $newpairs = @($($pair[0] + $extra), $($extra + $pair[1]))
            $newstate[$pair] -= $quantity
            $newstate[$($newpairs[0])] += $quantity
            $newstate[$($newpairs[1])] += $quantity
        }
        
    }

    $countpolymerpairs = $newstate
   
}   

foreach($pair in $countpolymerpairs.keys){
    $endcountletter[[string]$pair[0]] += $countpolymerpairs[$pair]
    $endcountletter[[string]$pair[1]] += $countpolymerpairs[$pair]
}

#correction start and end
$startletter = [string]$startstring[0]
$endletter = [string]$startstring[$($startstring.Length -1)]
$endcountletter[$startletter] ++
$endcountletter[$endletter] ++

[long]$lowest = $([long]::MaxValue)
[long]$highest = 0

foreach($letter in $endcountletter.keys){
[long]$number =  $endcountletter[$letter] /2
   if($number -lt $lowest){
       $lowest = $number
   }
   if($number -gt $highest){
       $highest = $number
   }
}

$highest - $lowest