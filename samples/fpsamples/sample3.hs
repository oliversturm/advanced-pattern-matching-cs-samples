-- This function uses a boolean guard and defines
-- its value only for the remaining cases.
fact :: Int -> Int
fact x | x <= 1 = 1
       | otherwise = x * (fact (x - 1))

-- Alternatively, pattern matching can be used for an
-- even more explicit declaration.
fact' :: Int -> Int
fact' 0 = 1
fact' 1 = 1
fact' x = x * (fact' (x - 1))
