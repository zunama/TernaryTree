AutocompleteTesting: Little windows application to test the lookup speed. UI is not threaded.



TernaryTree: This is commandline / library class for creating a word tree.

This is a data structure for holding words to use for autocomplete.

It is O(log n) for searching the list for prefix node (starting letters) and the O(n) to compile the word list visiting each node past the found prefix node. Adding a word to the list is linear as well and memory space is minimized with the small node structure. Single character plus 3 references and boolean value.

The sample data is about 350,000 words found in the WordList.txt.

Using a MA-FSA structure will perform just as fast but memory footprint is much smaller.