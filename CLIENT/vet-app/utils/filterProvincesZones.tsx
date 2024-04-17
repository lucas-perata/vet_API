import { BsAsAreasLists } from "./lists";

export const getAreasByProvince = (province: string) => {
  // TODO: add areas to each province
  const areasMap = {
    "Buenos Aires": BsAsAreasLists,
    "Córdoba": ["Villa María", "Río Cuarto", "Córdoba Capital", "Alta Gracia", "Jesús María", "Villa Allende", "Villa Carlos Paz", "La Calera"],
    "Santa Fe": ["Rosario", "Santa Fe Capital", "Rafaela", "Venado Tuerto", "Reconquista", "Esperanza", "Las Rosas", "Casilda"],
    "Mendoza": ["Mendoza Capital", "Godoy Cruz", "Guaymallén", "Maipú", "Luján de Cuyo", "San Martín", "Las Heras", "San Rafael"],
    "Tucumán": ["San Miguel de Tucumán", "Tafí Viejo", "Yerba Buena", "Banda del Río Salí", "Alderetes", "Famaillá", "Aguilares", "Concepción"],
    "Salta": ["Salta Capital", "Tartagal", "Orán", "Cafayate", "General Güemes", "Rosario de la Frontera", "Metán", "Embarcación"],
    "Chaco": ["Resistencia", "Barranqueras", "Presidencia Roque Sáenz Peña", "Villa Ángela", "Quitilipi", "Castelli", "Las Breñas", "Charata"],
    "Entre Ríos": ["Paraná", "Concordia", "Gualeguaychú", "Villaguay", "Gualeguay", "Chajarí", "Federación", "Victoria"]
  };

  return areasMap[province] || [];
};
