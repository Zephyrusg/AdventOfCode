$path = "D:\Personal Storage\Bas\Git\AdventofCode\2021"
$data = Get-Content -Path "$($path)\inputDay14"
#$data = Get-Content -Path "$($path)\Exampleday14"

$startstring = $data[0]

$keys =  $data[2..$($data.Count-1)]

$linkedList  = new-object System.Collections.Generic.linkedlist[string]

$polymertemTemplates = @{}

foreach($template in $keys){
    $polymertemTemplates.Add($template.Substring(0,2),$template[-1])
}

For($i=0;$i -lt $startstring.Length;$i++){
    $linkedList.Add($startstring[$i])
}

$times = 10

$last = $linkedList.Last

for($i=0; $i -lt $times;$i++){

    $first = $linkedList.First
    $previous = $first
    $next = $previous.Next
    :loop for($j=0;$j -lt $linkedList.count -1;$j++ ){
        $created = [string]$polymertemTemplates[$($previous.Value + $next.Value)] 
        $null = $linkedList.AddAfter($previous, $created)
        if($next -eq $last){
            break loop
        }
        $next = $next.Next
        $previous = $next.Previous   
    }   
}
$elements = ($linkedList | Select-Object -Unique)


[int]$lowest = $([int32]::MaxValue)
$highest = 0

foreach($element in $elements){
   $number =  ($linkedList | Select-Object | Where-Object{ $_ -eq $element}).count
   if($number -lt $lowest){
       $lowest = $number
   }
   if($number -gt $highest){
       $highest = $number
   }
}

$highest - $lowest

