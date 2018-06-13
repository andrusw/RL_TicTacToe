RL_TicTacToe  
============
check out the wiki: https://github.com/andrusw/RL_TicTacToe/wiki

HISTORY
=======
I originally started this project with a command line program, and then later converted it into a buggy, 
ugly user friendly GUI in Window's Form using Framework 1.1.4322 SP1. Later I would barley upgrade the 
code with newer version of Visual Studio, only making the necessary changes to get the code to compile. 


REINFORCEMENT LEARNING
======================
Reinforcement learning is the process for a program to interact within an environment and 
learn by receiving numerical rewards or punishment while trying to obtain a goal. In this case, 
the goal is to win when dealing with tic-tac-toe. The environment is “where” and “how” a program, 
that is using reinforcement learning, interacts against its opponents in the game. The reward/punishment 
is a scalar value. Depending on the program’s decisions and actions, it will make a move and recieve an
reward later.


DYNAMIC PROGRAMMING
===================
In dynamic programming the reward for each move is based upon all the possible values that could have
been taken. This concept is called a full backup, where each move is given a reward after is has been
taken.  

MONTE CARLO METHOD
==================
In monte carlo the reward for each of the moves made in the game is based upon the ending result, where
each of the moves would be given a reward, when the game has finished.

TEMPORAL DIFFERENCE
===================
In temporal difference the reward is given after each move (like Dynamic Programming), but the reward is
based on the move that was made (like monte carlo method).

