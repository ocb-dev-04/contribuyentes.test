import axiosClient from "./axiosClient";
import type { PaginatedCollection } from "../types/PaginatedCollection";
import type { Registro } from "../types/Registro";

export const getRegistrosByContribuyente = async (contribuyenteId: string, page: number) => {
  const res = await axiosClient.get<PaginatedCollection<Registro>>(
    `/registros?contribuyenteId=${contribuyenteId}&pageNumber=${page}`
  );
  return res.data;
};

export const getTotalItbisSum = async (contribuyenteId: string) => {
  const res = await axiosClient.get<{ totalSum: number }>(
    `/registros/total-itbs-sum?contribuyenteId=${contribuyenteId}`
  );
  return res.data.totalSum;
};
