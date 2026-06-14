import { useEffect, useState } from 'react'
import axios from 'axios'
import { getSupportedUnits, convert, SupportedUnits, ConvertResponse } from './api'
import './App.css'

type Category = 'length' | 'weight' | 'temperature'

const CATEGORY_LABELS: Record<Category, string> = {
  length:      'Length',
  weight:      'Weight',
  temperature: 'Temperature',
}

export default function App() {
  // Unit discovery from API
  const [units, setUnits] = useState<SupportedUnits | null>(null)
  const [loadingUnits, setLoadingUnits] = useState(true)
  const [unitsError, setUnitsError] = useState<string | null>(null)

  // Form state
  const [category, setCategory] = useState<Category>('length')
  const [fromUnit, setFromUnit] = useState('')
  const [toUnit, setToUnit] = useState('')
  const [value, setValue] = useState('')

  // Result state
  const [result, setResult] = useState<ConvertResponse | null>(null)
  const [loading, setLoading] = useState(false)
  const [error, setError] = useState<string | null>(null)

  // Fetch supported units on mount
  useEffect(() => {
    getSupportedUnits()
      .then(data => {
        setUnits(data)
        setFromUnit(data.length[0] ?? '')
        setToUnit(data.length[1] ?? '')
      })
      .catch(err => {
        setUnitsError('Could not load units from API. Is the backend running?')
        console.error(err)
      })
      .finally(() => setLoadingUnits(false))
  }, [])

  // When category changes, reset unit selections to the first two in that category
  useEffect(() => {
    if (!units) return
    const list = units[category]
    setFromUnit(list[0] ?? '')
    setToUnit(list[1] ?? '')
    setResult(null)
    setError(null)
  }, [category, units])

  const currentUnits = units ? units[category] : []

  async function handleConvert(e: React.FormEvent) {
    e.preventDefault()

    const num = parseFloat(value)
    if (isNaN(num)) {
      setError('Please enter a valid number.')
      return
    }

    setLoading(true)
    setError(null)
    setResult(null)

    try {
      const res = await convert({ value: num, fromUnit, toUnit })
      setResult(res)
    } catch (err) {
      if (axios.isAxiosError(err)) {
        // Surface the API's { message } error body
        const msg = err.response?.data?.message ?? err.response?.data ?? err.message
        setError(String(msg))
      } else {
        setError('An unexpected error occurred.')
      }
    } finally {
      setLoading(false)
    }
  }

  // ── Render ────────────────────────────────────────────────────────────

  if (loadingUnits) {
    return (
      <div className="page">
        <p className="status-msg">Loading units from API…</p>
      </div>
    )
  }

  if (unitsError) {
    return (
      <div className="page">
        <p className="error-msg">{unitsError}</p>
      </div>
    )
  }

  return (
    <div className="page">
      <div className="card">
        <h1 className="title">Unit Conversion</h1>

        <form onSubmit={handleConvert} noValidate>

          {/* Category */}
          <div className="field">
            <label htmlFor="category" className="label">Category</label>
            <select
              id="category"
              className="input"
              value={category}
              onChange={e => setCategory(e.target.value as Category)}
            >
              {(Object.keys(CATEGORY_LABELS) as Category[]).map(cat => (
                <option key={cat} value={cat}>{CATEGORY_LABELS[cat]}</option>
              ))}
            </select>
          </div>

          {/* Value */}
          <div className="field">
            <label htmlFor="value" className="label">Value</label>
            <input
              id="value"
              type="number"
              className="input"
              value={value}
              onChange={e => setValue(e.target.value)}
              placeholder="Enter a number"
              step="any"
              required
            />
          </div>

          {/* From Unit */}
          <div className="field">
            <label htmlFor="fromUnit" className="label">From</label>
            <select
              id="fromUnit"
              className="input"
              value={fromUnit}
              onChange={e => setFromUnit(e.target.value)}
            >
              {currentUnits.map(u => (
                <option key={u} value={u}>{u}</option>
              ))}
            </select>
          </div>

          {/* To Unit */}
          <div className="field">
            <label htmlFor="toUnit" className="label">To</label>
            <select
              id="toUnit"
              className="input"
              value={toUnit}
              onChange={e => setToUnit(e.target.value)}
            >
              {currentUnits.map(u => (
                <option key={u} value={u}>{u}</option>
              ))}
            </select>
          </div>

          <button type="submit" className="btn" disabled={loading}>
            {loading ? 'Converting…' : 'Convert'}
          </button>
        </form>

        {/* Error */}
        {error && (
          <div className="error-box" role="alert">
            {error}
          </div>
        )}

        {/* Result */}
        {result && (
          <div className="result-box" role="status">
            <span className="result-value">{result.convertedValue}</span>
            <span className="result-unit">{result.toUnit}</span>
          </div>
        )}
      </div>
    </div>
  )
}
