import React, {
  createContext,
  useContext,
  useState,
  ReactNode,
  useEffect,
} from "react";
import User from "../types/User";

interface AuthContextType {
  user: User | null;
  isLogged: boolean;
  login: (userData: User, token: string) => Promise<void>;
  logout: () => void;
}

export const AuthContext = createContext<AuthContextType | undefined>(
  undefined
);

export const useAuth = () => {
  const context = useContext(AuthContext);
  if (!context) {
    throw new Error("useAuth deve ser usado dentro de um AuthProvider");
  }
  return context;
};

interface AuthProviderProps {
  children: ReactNode;
}

export const AuthProvider: React.FC<AuthProviderProps> = ({ children }) => {
  const [user, setUser] = useState<User | null>(null);
  const [isLogged, setIsLogged] = useState<boolean>(false);

  const login = async (userData: User, token: string) => {
    setUser(userData);
    setIsLogged(true);
    localStorage.setItem("@token", token);
  };

  const logout = () => {
    localStorage.removeItem("@token");
    setUser(null);
    setIsLogged(false);
  };

  useEffect(() => {
    const token = localStorage.getItem("@token");

    if (token) {
      setIsLogged(true);
    }
  }, []);

  return (
    <AuthContext.Provider value={{ user, login, logout, isLogged }}>
      {children}
    </AuthContext.Provider>
  );
};
