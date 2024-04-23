import React, { useEffect, useState } from "react";
import { Button } from "devextreme-react/button";
import { TextBox, TextBoxTypes } from "devextreme-react/text-box";
import { CheckBox } from "devextreme-react/check-box";
import ApiRoutes from "../../api";
import { useAuth } from "../../context/AuthContext";
import { useNavigate } from "react-router-dom";

const SignIn: React.FC = () => {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");

  const navigate = useNavigate();

  const { login, isLogged } = useAuth();
  const handleSubmit = async (e: any) => {
    e.preventDefault();

    const { data } = await ApiRoutes.signIn({ email, password });

    if (data.token) {
      await login(data.user, data.token);
    }
  };

  useEffect(() => {
    if (isLogged) {
      navigate("/dashboard");
    }
  }, [isLogged]);

  return (
    <div
      style={{
        display: "flex",
        justifyContent: "center",
        alignItems: "center",
        height: "100vh",
        background: "#fff",
      }}
    >
      <form
        onSubmit={handleSubmit}
        style={{
          background: "#f1f1f1",
          borderRadius: 20,
          padding: "80px 100px",
          display: "flex",
          flexDirection: "column",
          gap: 10,
          boxShadow: "rgba(0, 0, 0, 0.35) 0px 5px 15px",
        }}
      >
        <h2>Sign In</h2>
        <TextBox
          onValueChanged={(data: TextBoxTypes.ValueChangedEvent) => {
            setEmail(data.value);
          }}
          value={email}
          placeholder="Email"
          name="email"
        />
        <TextBox
          value={password}
          mode="password"
          placeholder="Password"
          name="password"
          onValueChanged={(data: TextBoxTypes.ValueChangedEvent) => {
            setPassword(data.value);
          }}
        />
        <div style={{ display: "flex", flexDirection: "column", gap: 10 }}>
          <CheckBox text="Remember me" defaultValue={false} name="rememberMe" />
          <Button type="success" text="Sign In" useSubmitBehavior={true} />
        </div>
      </form>
    </div>
  );
};

export default SignIn;
