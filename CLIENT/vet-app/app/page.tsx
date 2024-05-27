"use client"
import Image from 'next/image'
import React, { useState, useEffect } from 'react';
import { Label } from "@/components/ui/label"
import { Switch } from "@/components/ui/switch"
import missionImg from "../public/istockphoto-1320254199-1024x1024.jpg";

export default function Home() {

  const [isSwitchOn, setSwitchOn] = useState(false);
  const [showDiv, setShowDiv] = useState(false);

  const handleSwitchChange = () => {
    setSwitchOn(!isSwitchOn);
  };

  useEffect(() => {
    const handleScroll = () => {
      const scrollHeight = document.documentElement.scrollHeight;
      const scrollTop = document.documentElement.scrollTop;
      const clientHeight = document.documentElement.clientHeight;

      if (scrollTop + clientHeight >= scrollHeight * 0.8) {
        setShowDiv(true);
      } else {
        setShowDiv(false);
      }
    };

    window.addEventListener('scroll', handleScroll);
    return () => window.removeEventListener('scroll', handleScroll);
  }, []);

 const [isVisible, setIsVisible] = useState(false);

const toggleVisibility = () => {
    if (window.pageYOffset > 300){
      setIsVisible(true);
    }
  };

  useEffect(() => {
    window.addEventListener('scroll', toggleVisibility);

    return () => {
      window.removeEventListener('scroll', toggleVisibility);
    };
  }, []);

  return (
    <div className="flex flex-col w-full h-full p-0 m-0">
      <div className="flex flex-col justify-around bg-green-600 text-white min-w-full h-[90vh] p-10">

        <div className="flex flex-col">
          {isSwitchOn ? <div className="flex flex-col items-center text-center">
            <h2 className="text-4xl font-bold">Potenciá tu práctica veterinaria</h2>
            <p className="w-80">Optimiza tu flujo de trabajo y brinda un servicio excepcional con nuestra plataforma diseñada para profesionales veterinarios. 
              Gestiona citas, accede a historiales médicos y mantén una comunicación fluida con tus clientes, todo en un solo lugar.</p>
          </div> :  <div className="flex flex-col items-center text-center">
            <h2 className="text-4xl font-bold">Cuida a tu mejor amigo como se merece</h2>
            <p className="w-80">Nuestra plataforma te brinda acceso a profesionales veterinarios de confianza y herramientas prácticas para cuidar de la salud y bienestar de tu mascota. 
                Desde citas médicas hasta consejos expertos, todo lo que necesitas está a un clic de distancia.</p>
          </div> }
          <div className="flex flex-col items-center">asd</div>
        </div>

        <div className="flex justify-center align-center gap-4 ">
            <p>{isSwitchOn ? 'Para profesionales de la salud' : 'Para amantes de las mascotas'}</p> 
            <div className='flex justify-center align-middle'>
               <Switch id="registrate como veterinario" onClick={handleSwitchChange}/>
            </div>
        </div>  
 
      </div>

      <div className="bg-green-50 h-[80vh]">
        <h2 className="text-2xl font-bold p-10 text-center">Servicios dueños</h2>
        <div className="flex justify-around p-10">

   {isVisible && <div className="animate-fadeIn">Servicio</div>}
          {isVisible && <div className="animate-fadeIn">Servicio</div>}
{isVisible && <div className="animate-fadeIn">Servicio</div>}
      {isVisible && <div className="animate-fadeIn">Servicio</div>}
        </div>

      </div>
<div className="h-[80vh]">
        <h2 className="text-2xl font-bold p-10 text-center">Servicios Profesionales</h2>
        <div className="flex justify-around p-10">
             {isVisible && <div className="animate-fadeIn">Servicio</div>}
          {isVisible && <div className="animate-fadeIn">Servicio</div>}
{isVisible && <div className="animate-fadeIn">Servicio</div>}
      {isVisible && <div className="animate-fadeIn">Servicio</div>}        </div>
      </div>
      <div className="bg-green-500 h-[80vh] flex flex-row justify-center">
      {isVisible &&  <div className="bg-white self-center p-10 mt-10 mb-10 rounded-xl shadow-xl w-[60vw] h-[70vh] animate-fadeIn">
 
          <div className="flex flex-col items-center">
            <img width={400} height={400} src="https://media.istockphoto.com/id/1320254199/es/vector/una-mujer-divirti%C3%A9ndose-jugando-con-su-gato-y-su-perro.jpg?s=2048x2048&w=is&k=20&c=dnHe-jf3TcwBbCeXPmaX6R8gVgUxTlZy7rmOBERWgOw=" alt=""/>
        <div className="flex gap-4 flex-col items-center">
  
              <h3 className="font-bold text-2xl text-center ">Queremos crear una plataforma para ayudar a los amantes de las mascotas y a los veterinarios a conectar</h3>
          <p className="w-[40vw] text-center">
            Lorem ipsum dolor sit amet consectetur adipiscing elit, nulla purus facilisis sollicitudin dictumst integer laoreet varius Ultricies 
              imperdiet dictum scelerisque mollis faucibus odio, pellentesque sodales luctus inceptos ligula donec libero eleifend velit, 
              elementum vehicula neque netus blandit a parturient quam. Tortor in nisl neque lobortis augue quis quisque
          </p>
            </div> 

          </div>        

        </div>} 
      </div>
      <div className="flex justify-around bg-green-50 p-10">
        <div><h2 className="text-gray-400 mb-3">Servicios</h2>
          <ul>
            <li>Servicio 1</li>
            <li>Servicio 2</li>
            <li>Servicio 3</li>
          </ul>
        </div>
        <div><h2 className="text-gray-400 mb-3">Soluciones</h2>
          <ul>
            <li>Soluciones 1</li>
            <li>Soluciones 2</li>
            <li>Soluciones 3</li>
          </ul>
        </div>
        <div><h2 className="text-gray-400 mb-3">Nosotros</h2>
          <ul>
            <li>Misión            </li>
            <li>Objetivos</li>
            <li>Blog</li>
          </ul>
        </div>
        <div><h2 className="text-gray-400 mb-3">Contacto</h2>
          <ul>
            <li>Telefono</li>
            <li>Mail</li>
            <li>Redes</li>
          </ul>
        </div>
      </div>
    </div >
  );
}
