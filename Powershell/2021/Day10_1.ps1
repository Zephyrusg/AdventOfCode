$path = "D:\Personal Storage\Bas\Git\AdventofCode\2021"
$data = Get-Content -Path "$($path)\inputDay10"
#$data = Get-Content -Path "$($path)\Exampleday10"

$openchars = @('(','{','[','<')
$closedchars = @(')','}',']','>')
$correctpairs = @('()','{}','[]','<>')

$countInvalidChars = @{
    ')' = 0
    ']' = 0
    '}' = 0
    '>' = 0
}

foreach($item in $data){
    $currentOpenChars = New-Object Collections.Generic.List[string]
    $line = $item.ToCharArray()
    :loop Foreach($char in $line){
        if($openchars -contains $char){
            $currentOpenChars.Add($char)
        }
        elseif($closedchars -contains $char){
            $pair = $currentOpenChars[-1] + $char
            if($correctpairs -contains $pair){
                $currentOpenChars.RemoveAt($($currentOpenChars.Count) -1)
            }else{
                $countInvalidChars[[string]$char]++
                break loop
            }

        }
    }
}

[int]$score = 0

foreach($key in $countInvalidChars.Keys){
    switch($key){
        ')'{$score += [int](3 * $countInvalidChars[$key]) }
        '}'{$score += [int](57 * $countInvalidChars[$key])}
        '}'{$score += [int](1197 * $countInvalidChars[$key])}
        '>'{$score += [int](25137 * $countInvalidChars[$key])}
    }
}

$score