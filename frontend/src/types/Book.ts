export default interface Book {
  bookId?: number;
  title: string;
  Author: string;
  totalCopies: number;
  copiesInUse: number;
  type: string;
  category: string;
}
