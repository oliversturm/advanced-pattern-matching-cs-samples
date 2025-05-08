-- In Haskell, patterns are used to define function overloads.
-- This is a very "static" case.

data Food = Pasta | Pizza | Chips

isEqualFood :: Food -> Food -> Bool
isEqualFood Pasta Pasta = True
isEqualFood Pizza Pizza = True
isEqualFood Chips Chips = True
isEqualFood _ _ = False
