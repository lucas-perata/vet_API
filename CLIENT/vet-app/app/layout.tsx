"use client"
import type { Metadata } from "next";
import "./globals.css";
import Navbar from "./nav/Navbar";
import { Toaster } from "@/components/ui/toaster";
import TanstackProvider from "@/components/TanstackProvider";
import NextTopLoader from 'nextjs-toploader';
import { useParams, useSearchParams } from "next/navigation";
import NavbarApp from "./nav/NavbarApp";
import NavbarFront from "./nav/NavbarFront";
import useStore from "@/store/store";
import { getRole } from "./actions/getRole";



export default function RootLayout({

  children,
}: Readonly<{
  children: React.ReactNode;
}>) {

  let app: string | undefined;
  const params = useParams();
  app = params.app;

  const token = useStore((state) => state.token);
  const kookies = token();
  let role: string;
  let front: boolean = true;

  if (kookies) {
    role = getRole(kookies);
    front = false;
  }

  return (
    <html lang="en">
      <body style={{ minWidth: "98vw", maxWidth: "100%", height: "90vh" }}>
        <TanstackProvider>
          {app == "app" ? <NavbarApp kookies={kookies} role={role} /> : <NavbarFront />}
          <main className={(app == "app" ? "p-7" : "p-0")} style={{ minWidth: "98%", maxHeight: "90vh" }}>
            <NextTopLoader color="#4edd22" height={5} />
            {children}
          </main>
          <Toaster />
        </TanstackProvider>
      </body>
    </html >
  );
}
