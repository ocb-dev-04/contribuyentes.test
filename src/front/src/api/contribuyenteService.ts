import axiosClient from "./axiosClient";
import type { Contribuyente } from "../types/Contribuyente ";
import type { PaginatedCollection } from "../types/PaginatedCollection";

export const getContribuyentes = async (page = 1) => {
  const res = await axiosClient.get<PaginatedCollection<Contribuyente>>(
    `/contribuyentes?pageNumber=${page}`
  );
  return res.data;
};