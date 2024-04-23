import React, { ReactNode } from "react";
import { useAuth } from "../../context/AuthContext";
import { useNavigate } from "react-router-dom";

interface AuthWrapperProps {
  children: ReactNode;
}

const AuthWrapper: React.FC<AuthWrapperProps> = ({ children }) => {
  const { isLogged } = useAuth();
  const navigate = useNavigate();
  if (isLogged) {
    return <>{children}</>;
  } else {
    navigate("/");
    return <span>Without authorization</span>;
  }
};

export default AuthWrapper;
