internal class Sorting {
        internal static int[] SelectionSort(int[] unsortedArray) {
            //Minimum element is picked from left array and added to right side array.
            //But above is extra memory and not adviced hence in memory replace required called "In Place".
            //O(n^2) time complexity
            for (int j = 0; j < unsortedArray.Length -1; j++) {
                int minElement = 0;
                int indexForSwap = 0;
                for (int i = j; i < unsortedArray.Length; i++) {
                    if (i == j) {
                        indexForSwap = j;
                        minElement = unsortedArray[j];
                    }
                    if (minElement > unsortedArray[i]) {
                        indexForSwap = i;
                        minElement = unsortedArray[i];
                    }
                }
                unsortedArray[indexForSwap] = unsortedArray[j];
                unsortedArray[j] = minElement;
            }
            return unsortedArray;
        }

        internal static int[] BubbleSort(int[] unsortedArray) {
            //swap the consecutive value with higher by swaping 
            //and repeat it again until last is highest
            for (int j = 1; j < unsortedArray.Length -1 ; j++) {
                for (int i = 0; i < unsortedArray.Length-j; i++) {
                    int nextItem = i + 1;
                    if (unsortedArray[i] > unsortedArray[nextItem]) {
                        int tempVal = unsortedArray[i];
                        unsortedArray[i] = unsortedArray[nextItem];
                        unsortedArray[nextItem] = tempVal;
                    }
                }
            }
            return unsortedArray;
        }

        internal static int[] InsertionSort(int[] unsortedArray) {
            //In an unsorted array maintain two sections, one sorted another unsorted
            //Select item insert in sorted and shift other towards hole
            for (int i = 1; i < unsortedArray.Length; i++) {
                //create a hole to select value at pos for comparision
                int hole = i;
                int selectedValue = unsortedArray[i];
                int current = hole - 1;
                //Iterate till zero and till items are greater than selected value
                while (current >= 0 && unsortedArray[current] > selectedValue) {
                    //Shift one item each towards the hole
                    unsortedArray[current + 1] = unsortedArray[current];
                    //move towards first element in sorted array
                    current = current - 1;
                }
                //Consider above while has shifted the greater value
                //now fill the value in void created
                //current + 1 is done as just before coming out 
                //in while we did - 1, so +1 will retain back the position
                unsortedArray[current + 1] = selectedValue;
            }
            return unsortedArray;
        }
    }
