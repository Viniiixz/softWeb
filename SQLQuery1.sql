select sum(Amount) as [Valor], max(DateKey) as [Data] from dbo.FactFinance 
    where DateKey = '20101229' --somando por data específica
        group by DateKey