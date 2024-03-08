"use client";
import React, { useState, FormEvent } from "react";
import { login } from "../../../utils/auth";
import {
  CardTitle,
  CardDescription,
  CardHeader,
  CardContent,
  Card,
} from "@/components/ui/card";
import { Label } from "@/components/ui/label";
import { Input } from "@/components/ui/input";
import { Button } from "@/components/ui/button";
import { useToast } from "@/components/ui/use-toast";

const page: React.FC = () => {
  const [email, setEmail] = useState<string>("");
  const [password, setPassword] = useState<string>("");
  const [error, setError] = useState<string | null>(null);
  const { toast } = useToast();

  const handleSubmit = async (e: FormEvent) => {
    e.preventDefault();
    try {
      const user = await login({ email, password });
      toast({
        title: "Ingresaste",
      });
      window.location.href = "/dashboard";
    } catch (err: any) {
      setError(err.message);
      toast({
        title: "Error",
        description: err,
      });
    }
  };

  return (
    <form onSubmit={handleSubmit}>
      <Card className="mx-auto max-w-sm">
        <CardHeader className="space-y-1">
          <CardTitle className="text-2xl font-bold">
            Ingresa a tu cuenta
          </CardTitle>
          <CardDescription></CardDescription>
        </CardHeader>
        <CardContent>
          <div className="space-y-4">
            <div className="space-y-2">
              <Label htmlFor="email">Email</Label>
              <Input
                type="email"
                value={email}
                onChange={(e) => setEmail(e.target.value)}
                id="email"
                placeholder="m@example.com"
                required
              />
            </div>
            <div className="space-y-2">
              <Label htmlFor="password">Password</Label>
              <Input
                id="password"
                required
                type="password"
                value={password}
                onChange={(e) => setPassword(e.target.value)}
                placeholder="Password"
              />
            </div>
            <Button className="w-full" type="submit">
              Login
            </Button>
          </div>
        </CardContent>
      </Card>
    </form>
  );
};

export default page;
