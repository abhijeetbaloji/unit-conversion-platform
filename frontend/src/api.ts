import axios from 'axios'

const API_URL = import.meta.env.VITE_API_URL

export interface ConvertRequest {
  value: number
  fromUnit: string
  toUnit: string
}

export interface ConvertResponse {
  originalValue: number
  fromUnit: string
  toUnit: string
  convertedValue: number
}

export interface SupportedUnits {
  length: string[]
  weight: string[]
  temperature: string[]
}

export async function getSupportedUnits(): Promise<SupportedUnits> {
  const res = await axios.get<SupportedUnits>(`${API_URL}/api/conversions/supported-units`)
  return res.data
}

export async function convert(req: ConvertRequest): Promise<ConvertResponse> {
  const res = await axios.post<ConvertResponse>(`${API_URL}/api/conversions`, req)
  return res.data
}
