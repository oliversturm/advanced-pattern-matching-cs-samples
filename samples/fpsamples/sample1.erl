% Message passing in Erlang - the "add" and "mult"
% identifiers are "atoms", constant values that are
% used to match incoming messages (which are technically
% tuples).

loop() ->
    receive
        {add, A, B} -> 
            io:format("adding: ~p~n", [A + B]),
            loop();
        {mult, A, B} -> 
            io:format("multiplying: ~p~n", [A * B]),
            loop()
    end.
