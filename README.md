# Virtual Recall Screening Test

There are 3 short parts to complete.  
Feel free to use online resources where appropriate. Try to keep solutions succinct, efficient and well engineered.

Part 1) Please submit the SQL query to return the requested data from question 1.  
The proposed database alterations of question 2 can be supplied as DDL statements along with any explanations you feel appropriate.

## Answer - Test 1

```SQL
--SQL QUERY
DECLARE @sku NVARCHAR(max) = 'Heart'

SELECT a.Id AS AnimalId, a.Name AS AnimalName, a.Deceased As AnimalStatus, cl.Id AS ClinetId, CONCAT(cl.Forename, ' ', cl.Surname) AS ClinetName, cl.Active AS ClinetStatus
FROM Animal (NOLOCK) a
INNER JOIN ClientAnimal (NOLOCK) ca on ca.AnimalId = a.Id
INNER JOIN Client (NOLOCK) cl on cl.Id = ca.ClientId
INNER JOIN Treatment (NOLOCK) tr on tr.AnimalId = a.Id
INNER JOIN Product (NOLOCK) p on p.Id = tr.ProductId
WHERE p.SKU = @sku
-- I couldn't not connect to you database server so I am sure about the schema structure. So, I just added to make sure that if there is null data and consider it as well.
AND ISNULL(a.Deceased, 0) = 0
AND ISNULL(cl.Active, 0) = 1


-- FOR IMPORVEMENT
CREATE NONCLUSTERED INDEX [IX_dbo.ClientAnimal_AnimalId] ON ClientAnimal (AnimalId)
CREATE NONCLUSTERED INDEX [IX_dbo.ClientAnimal_ClientId] ON ClientAnimal (ClientId)
CREATE NONCLUSTERED INDEX [IX_dbo.Product_Sku] ON Product (SkU)

-- FOR FURTHER IMPORVMENT
-- I would suggest that we should have Clinet Id refrence in Animals table only if an animal is not owned by more than clients
-- This is just to avoid multiple joins we can say it as database denormalization
```

Part 2) Coding challenge. There are a few functions to implement. Please submit the .cs file with functions implemented. The tests should all pass.

## Answer - Test 2

Just got to the dir '__[base dir of your machine]\VirtualRecallTest\Src\VirtualRecall\VirtualRecall.Application.Tests__' and run comand '__dotnet test__' to check all test are passed.

```C#
/// <summary>
/// Gets all points x2 + y2 less than equal to 1 = number of points inside circle
/// Gives approximation for given points as Pi/4
/// </summary>
/// <param name="pts"></param>
/// <returns></returns>
public static double Approx(Point[] pts)
{
    var numberOfPointsInCircle = (double)pts.Select(p => Math.Pow(p.X, 2) + Math.Pow(p.Y2)).Count(xyScore => xyScore <= 1);
    return (numberOfPointsInCircle / pts.Count()) * 4;
}

/// <summary>
/// Give average of input numerics
/// </summary>
/// <param name="set"></param>
/// <returns></returns>
public static double Average(IEnumerable<IHasNumeric> set)
{
    return set.Average(x => x.Num);
}

public static IEnumerable<int> GetPanelArrays(int numPanels)
{
    var result = new List<int>();
    var numPanelsCounter = numPanels;
    do
    {
        var panelSquare = GetSquarePanel(numPanelsCounter);
        result.Add(panelSquare);
        numPanelsCounter -= panelSquare;
    } while (numPanelsCounter > 0);

    static int GetSquarePanel(int numPanelsCounter)
    {
        var floor = (int)Math.Floor(Math.Sqrt(numPanelsCounter));
        return (int)Math.Pow(floor, 2);
    
    return result;
}

/// <summary>
/// Return false if none of the requested capabilities have been granted, otherwise true.
/// </summary>
/// <param name="granted">Set of flags indicated which capabilities have been granted.param>
/// <param name="requested">Which capabilities are being requested.</param>
/// <returns> Whether ANY of the required capabilities have been granted. Also return truif none is requested.
/// </returns>
public static bool AnyGranted(Capabilities granted, Capabilities requested)
{
    if (requested == Capabilities.None)
        return true;

    var capabilities = Enum.GetValues(typeof(Capabilities));
    return capabilities.Cast<Capabilities>().Where(capability => capability !Capabilities.None)
        .Any(capability => requested.HasFlag(capability) && granted.HasFlag(capability));
}
```

Part 3) Some front-end JS and CSS. Please submit the .html file with the changes made into the placeholders.

## Answer - Test 3

```HTML
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01//EN">
<html lang="en">
<head>
    <title>Virtual Recall | Front End Test</title>

    <style type="text/css">
        .a, .c {
            color: black;
        }

        .b {
            color: red;
        }
    </style>
</head>
<body>
<!-- Without changing anything below this line -->
<div id="somediv">
    <p class="a">Leave this black</p>
    <p class="b">Make this red</p>
    <!-- It is considered as best practise to have a class with prefix js- to use in javascript when working in team oriented environment where UI?UX and Front Developer works in teams -->
    <p class="c js-c">Make this sentence green when <a id="colorClicker" href="javascript:void(0);">this</a> link is clicked</p>
</div>

<script type="text/javascript" src="https://code.jquery.com/jquery-2.1.4.min.js"></script>
<script>
    $(function () {
        $('#colorClicker').on('click', (e) => {
            $('.js-c').css('color', 'green');
        });
    });
</script>
</body>
</html>
```
