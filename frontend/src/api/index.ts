import { AxiosPromise } from "axios";
import axios from "./axios";
import Book from "../types/Book";

const ApiRoutes = {
  signIn: async ({
    email,
    password,
  }: {
    email: string;
    password: string;
  }): Promise<AxiosPromise> => {
    return await axios.post("/Authentication/SignIn", { email, password });
  },
  getBooks: async (): Promise<AxiosPromise> => {
    const token = localStorage.getItem("@token") || "";
    return await axios.get("/Books", {
      headers: { Authorization: `Bearer ${token}` },
    });
  },
  addBook: async (book: Book): Promise<AxiosPromise> => {
    const token = localStorage.getItem("@token") || "";
    return await axios.post("/Books/Create", book, {
      headers: { Authorization: `Bearer ${token}` },
    });
  },
};

export default ApiRoutes;
