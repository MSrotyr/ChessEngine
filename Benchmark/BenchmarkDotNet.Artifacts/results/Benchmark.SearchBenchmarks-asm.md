## .NET 10.0.7 (10.0.7, 10.0.726.21808), X64 RyuJIT x86-64-v4 (Job: DefaultJob)

```assembly
; Benchmark.SearchBenchmarks.GetPossibleMovesBenchmark()
       push      rbp
       push      r15
       push      rbx
       sub       rsp,90
       lea       rbp,[rsp+0A0]
       mov       rdi,offset MT_ChessEngine.Board
       call      CORINFO_HELP_NEWSFAST
       mov       rbx,rax
       mov       rdi,offset MT_System.Collections.Generic.Stack<BitBoards>
       call      CORINFO_HELP_NEWSFAST
       mov       rdi,7F23CD40A058
       mov       [rax+8],rdi
       lea       rdi,[rbx+8]
       mov       rsi,rax
       call      CORINFO_HELP_ASSIGN_REF
       lea       r15,[rbx+10]
       lea       rdi,[rbp-98]
       call      qword ptr [7F3365546910]; BitBoards.StartPosition()
       vmovdqu32 zmm0,[rbp-98]
       vmovdqu32 [r15],zmm0
       vmovdqu32 zmm0,[rbp-58]
       vmovdqu32 [r15+40],zmm0
       mov       rsi,[rbp-18]
       mov       [r15+80],rsi
       mov       rsi,7F23F1400378
       mov       rsi,[rsi]
       mov       rdi,rbx
       call      qword ptr [7F33655468E0]; ChessEngine.Search.GetPossibleMoves(ChessEngine.Board, ChessEngine.Move[])
       nop
       vzeroupper
       add       rsp,90
       pop       rbx
       pop       r15
       pop       rbp
       ret
; Total bytes of code 177
```
```assembly
; BitBoards.StartPosition()
       push      rbp
       mov       rbp,rsp
       vmovups   zmm0,[7F3364AB4040]
       vmovups   [rdi],zmm0
       vmovups   zmm0,[7F3364AB4080]
       vmovups   [rdi+40],zmm0
       mov       dword ptr [rdi+80],1010101
       mov       rax,rdi
       vzeroupper
       pop       rbp
       ret
; Total bytes of code 55
```
```assembly
; ChessEngine.Search.GetPossibleMoves(ChessEngine.Board, ChessEngine.Move[])
       push      rbp
       push      r15
       push      r14
       push      r13
       push      r12
       push      rbx
       sub       rsp,2E8
       lea       rbp,[rsp+310]
       mov       rbx,rdi
       mov       r15,rsi
       mov       rdi,offset MT_ChessEngine.Move[]
       mov       esi,100
       call      CORINFO_HELP_NEWARR_1_VC
       mov       r14,rax
       mov       rdi,[rbx+8]
       mov       r13d,[rdi+10]
       and       r13d,1
       mov       [rbp-2C],r13d
       mov       rdi,[rbx+80]
       mov       r12,rdi
       not       r12
       lea       rax,[rbx+10]
       mov       [rbp-2F0],rax
       mov       rdi,rax
       mov       rcx,[rdi]
       mov       [rbp-208],rcx
       mov       rdx,[rdi+8]
       mov       [rbp-210],rdx
       mov       rsi,[rdi+10]
       mov       [rbp-218],rsi
       mov       r8,[rdi+18]
       mov       [rbp-220],r8
       mov       r9,[rdi+20]
       mov       [rbp-228],r9
       mov       r10,[rdi+28]
       mov       [rbp-230],r10
       mov       r11,[rdi+30]
       mov       [rbp-238],r11
       mov       rcx,[rdi+38]
       mov       rdx,[rdi+40]
       mov       [rbp-240],rdx
       mov       rsi,[rdi+48]
       mov       [rbp-248],rsi
       mov       r8,[rdi+50]
       mov       [rbp-250],r8
       mov       r9,[rdi+58]
       mov       [rbp-258],r9
       mov       r10,[rdi+60]
       mov       [rbp-260],r10
       mov       rdi,[rdi+68]
       mov       [rbp-268],rdi
       test      r13d,r13d
       je        near ptr M02_L33
       not       rdi
       mov       [rbp-60],rdi
       mov       rdx,7F7F7F7F7F7F7F7F
       and       rdx,r11
       shr       rdx,7
       mov       [rbp-70],rdx
       mov       rdi,rcx
       call      qword ptr [7F3365546B80]; ChessEngine.Search.GenAllKnightMoves(UInt64)
       mov       rcx,0FEFEFEFEFEFEFEFE
       and       rcx,[rbp-238]
       shr       rcx,9
       or        rcx,[rbp-70]
       or        rax,rcx
       mov       [rbp-2F8],rax
       mov       rdi,[rbp-240]
       mov       rsi,[rbp-60]
       mov       rdx,[rbp-260]
       call      qword ptr [7F3365546C10]; ChessEngine.Search.GenBishopMoves(UInt64, UInt64, UInt64)
       or        rax,[rbp-2F8]
       mov       [rbp-58],rax
       mov       rdi,[rbp-248]
       mov       rsi,[rbp-60]
       mov       rdx,[rbp-260]
       call      qword ptr [7F3365546C28]; ChessEngine.Search.GenRookMoves(UInt64, UInt64, UInt64)
       or        rax,[rbp-58]
       mov       [rbp-58],rax
       mov       rdi,[rbp-250]
       mov       rsi,[rbp-60]
       mov       rdx,[rbp-260]
       call      qword ptr [7F3365546C10]; ChessEngine.Search.GenBishopMoves(UInt64, UInt64, UInt64)
       or        rax,[rbp-58]
       mov       [rbp-2F8],rax
       mov       rdi,[rbp-250]
       mov       rsi,[rbp-60]
       mov       rdx,[rbp-260]
       call      qword ptr [7F3365546C28]; ChessEngine.Search.GenRookMoves(UInt64, UInt64, UInt64)
       or        rax,[rbp-2F8]
       mov       [rbp-2F8],rax
       mov       rdi,[rbp-258]
       call      qword ptr [7F3365546B98]; ChessEngine.Search.GenAllBasicKingMoves(UInt64)
       or        rax,[rbp-2F8]
M02_L00:
       mov       [rbp-58],rax
       mov       [rbp-38],rax
       test      r13d,r13d
       jne       near ptr M02_L38
       mov       rdi,[rbx+70]
       mov       rax,rdi
       not       rax
       mov       [rbp-48],rax
       mov       rdi,[rbx+78]
       or        rdi,[rbx+88]
       mov       rsi,[rbx+10]
       xor       edx,edx
       test      rsi,rsi
       jne       near ptr M02_L30
       mov       r11d,edx
M02_L01:
       mov       rdi,[rbx+18]
       mov       esi,r11d
       test      rdi,rdi
       je        near ptr M02_L06
       mov       rdx,7F23F1401370
       jmp       short M02_L04
       nop       dword ptr [rax+rax]
M02_L02:
       mov       r10d,esi
M02_L03:
       blsr      rdi,rdi
       je        short M02_L07
       mov       esi,r10d
M02_L04:
       blsi      r8,rdi
       mov       r9,[rdx]
       xor       r10d,r10d
       tzcnt     r10,r8
       mov       r11d,[r9+8]
       cmp       r10,r11
       jae       near ptr M02_L40
       xor       r10d,r10d
       tzcnt     r10,r8
       mov       r11,rax
       and       r11,[r9+r10*8+10]
       je        short M02_L02
M02_L05:
       blsi      r9,r11
       lea       r10d,[rsi+1]
       cmp       esi,100
       jae       near ptr M02_L40
       mov       esi,esi
       lea       rsi,[rsi+rsi*2]
       lea       rsi,[r14+rsi*8+10]
       mov       [rsi],r8
       mov       [rsi+8],r9
       xor       r9d,r9d
       mov       [rsi+10],r9d
       blsr      r11,r11
       je        short M02_L03
       mov       esi,r10d
       jmp       short M02_L05
M02_L06:
       mov       r10d,esi
M02_L07:
       mov       rdi,[rbx+20]
       mov       rsi,[rbx+78]
       mov       [rbp-7C],r10d
       test      rdi,rdi
       je        near ptr M02_L10
M02_L08:
       blsi      r8,rdi
       mov       [rbp-78],r8
       mov       r9,rsi
       not       r9
       mov       r10,7F7F7F7F7F7F7F7F
       and       r10,r8
       mov       [rbp-2D0],r10
       mov       r11,r10
       shl       r11,9
       and       r11,rax
       mov       r12,7F7F7F7F7F7F7F7F
       and       r12,r11
       and       r12,r9
       shl       r12,9
       and       r12,rax
       mov       rcx,7F7F7F7F7F7F7F7F
       and       rcx,r12
       and       rcx,r9
       shl       rcx,9
       and       rcx,rax
       mov       rdx,7F7F7F7F7F7F7F7F
       and       rdx,rcx
       and       rdx,r9
       shl       rdx,9
       and       rdx,rax
       mov       r8,7F7F7F7F7F7F7F7F
       and       r8,rdx
       and       r8,r9
       shl       r8,9
       and       r8,rax
       mov       r10,7F7F7F7F7F7F7F7F
       and       r10,r8
       and       r10,r9
       shl       r10,9
       and       r10,rax
       or        r11,r12
       or        rcx,r11
       or        rdx,rcx
       or        rdx,r8
       or        rdx,r10
       mov       rcx,7F7F7F7F7F7F7F7F
       and       rcx,r10
       and       rcx,r9
       shl       rcx,9
       and       rcx,rax
       or        rdx,rcx
       mov       rcx,rsi
       not       rcx
       mov       r10,[rbp-2D0]
       shr       r10,7
       and       r10,rax
       mov       r8,7F7F7F7F7F7F7F7F
       and       r8,r10
       and       r8,rcx
       shr       r8,7
       and       r8,rax
       mov       r9,7F7F7F7F7F7F7F7F
       and       r9,r8
       and       r9,rcx
       shr       r9,7
       and       r9,rax
       mov       r11,7F7F7F7F7F7F7F7F
       and       r11,r9
       and       r11,rcx
       shr       r11,7
       and       r11,rax
       mov       [rbp-88],r11
       mov       r12,7F7F7F7F7F7F7F7F
       and       r12,r11
       and       r12,rcx
       shr       r12,7
       and       r12,rax
       mov       r11,7F7F7F7F7F7F7F7F
       and       r11,r12
       and       r11,rcx
       shr       r11,7
       and       r11,rax
       or        rdx,r10
       or        rdx,r8
       or        rdx,r9
       or        rdx,[rbp-88]
       or        rdx,r12
       or        rdx,r11
       mov       r8,7F7F7F7F7F7F7F7F
       and       r8,r11
       and       rcx,r8
       shr       rcx,7
       and       rcx,rax
       or        rdx,rcx
       mov       rcx,rsi
       not       rcx
       mov       r8,0FEFEFEFEFEFEFEFE
       and       r8,[rbp-78]
       mov       [rbp-2D8],r8
       mov       r10,r8
       shr       r10,9
       and       r10,rax
       mov       r11,0FEFEFEFEFEFEFEFE
       and       r11,r10
       and       r11,rcx
       shr       r11,9
       and       r11,rax
       mov       r12,0FEFEFEFEFEFEFEFE
       and       r12,r11
       and       r12,rcx
       shr       r12,9
       and       r12,rax
       mov       r9,0FEFEFEFEFEFEFEFE
       and       r9,r12
       and       r9,rcx
       shr       r9,9
       and       r9,rax
       mov       [rbp-90],r9
       mov       r8,0FEFEFEFEFEFEFEFE
       and       r8,r9
       and       r8,rcx
       shr       r8,9
       and       r8,rax
       mov       r9,0FEFEFEFEFEFEFEFE
       and       r9,r8
       and       r9,rcx
       shr       r9,9
       and       r9,rax
       or        rdx,r10
       or        rdx,r11
       or        rdx,r12
       or        rdx,[rbp-90]
       or        rdx,r8
       or        rdx,r9
       mov       r8,0FEFEFEFEFEFEFEFE
       and       r8,r9
       and       rcx,r8
       shr       rcx,9
       and       rcx,rax
       or        rdx,rcx
       mov       rcx,rsi
       not       rcx
       mov       r8,[rbp-2D8]
       shl       r8,7
       and       r8,rax
       mov       r9,0FEFEFEFEFEFEFEFE
       and       r9,r8
       and       r9,rcx
       shl       r9,7
       and       r9,rax
       mov       r10,0FEFEFEFEFEFEFEFE
       and       r10,r9
       and       r10,rcx
       shl       r10,7
       and       r10,rax
       mov       r11,0FEFEFEFEFEFEFEFE
       and       r11,r10
       and       r11,rcx
       shl       r11,7
       and       r11,rax
       mov       [rbp-98],r11
       mov       r12,0FEFEFEFEFEFEFEFE
       and       r12,r11
       and       r12,rcx
       shl       r12,7
       and       r12,rax
       mov       r11,0FEFEFEFEFEFEFEFE
       and       r11,r12
       and       r11,rcx
       shl       r11,7
       and       r11,rax
       or        rdx,r8
       or        rdx,r9
       or        rdx,r10
       or        rdx,[rbp-98]
       or        rdx,r12
       or        rdx,r11
       mov       r8,0FEFEFEFEFEFEFEFE
       and       r8,r11
       and       rcx,r8
       shl       rcx,7
       and       rcx,rax
       or        rdx,rcx
       jne       near ptr M02_L35
M02_L09:
       blsr      rdi,rdi
       jne       near ptr M02_L08
M02_L10:
       mov       rdi,[rbx+28]
       mov       rsi,[rbx+78]
       mov       r9d,[rbp-7C]
       mov       [rbp-9C],r9d
       test      rdi,rdi
       je        near ptr M02_L13
M02_L11:
       blsi      rcx,rdi
       mov       r8,rsi
       not       r8
       mov       r9,rcx
       shl       r9,8
       and       r9,rax
       mov       r10,r9
       and       r9,r8
       shl       r9,8
       and       r9,rax
       mov       r11,r9
       and       r9,r8
       shl       r9,8
       and       r9,rax
       mov       r12,r9
       and       r9,r8
       shl       r9,8
       and       r9,rax
       mov       [rbp-0A8],r9
       and       r9,r8
       shl       r9,8
       and       r9,rax
       mov       [rbp-0B0],r9
       and       r9,r8
       shl       r9,8
       and       r9,rax
       mov       rdx,r9
       or        r10,r11
       or        r10,r12
       or        r10,[rbp-0A8]
       or        r10,[rbp-0B0]
       or        rdx,r10
       and       r8,r9
       shl       r8,8
       and       r8,rax
       or        rdx,r8
       mov       r8,rsi
       not       r8
       mov       r9,7F7F7F7F7F7F7F7F
       and       r9,rcx
       add       r9,r9
       and       r9,rax
       mov       r10,7F7F7F7F7F7F7F7F
       and       r10,r9
       and       r10,r8
       add       r10,r10
       and       r10,rax
       mov       r11,7F7F7F7F7F7F7F7F
       and       r11,r10
       and       r11,r8
       add       r11,r11
       and       r11,rax
       mov       [rbp-0B8],r11
       mov       r12,7F7F7F7F7F7F7F7F
       and       r12,r11
       and       r12,r8
       add       r12,r12
       and       r12,rax
       mov       [rbp-0C0],r12
       mov       r11,7F7F7F7F7F7F7F7F
       and       r11,r12
       and       r11,r8
       add       r11,r11
       and       r11,rax
       mov       r12,7F7F7F7F7F7F7F7F
       and       r12,r11
       and       r12,r8
       add       r12,r12
       and       r12,rax
       or        rdx,r9
       or        rdx,r10
       or        rdx,[rbp-0B8]
       or        rdx,[rbp-0C0]
       or        rdx,r11
       or        rdx,r12
       mov       r9,7F7F7F7F7F7F7F7F
       and       r9,r12
       and       r8,r9
       add       r8,r8
       and       r8,rax
       or        rdx,r8
       mov       r8,rsi
       not       r8
       mov       r9,rcx
       shr       r9,8
       and       r9,rax
       mov       r10,r9
       and       r9,r8
       shr       r9,8
       and       r9,rax
       mov       r11,r9
       and       r9,r8
       shr       r9,8
       and       r9,rax
       mov       [rbp-0C8],r9
       and       r9,r8
       shr       r9,8
       and       r9,rax
       mov       [rbp-0D0],r9
       and       r9,r8
       shr       r9,8
       and       r9,rax
       mov       [rbp-0D8],r9
       and       r9,r8
       shr       r9,8
       and       r9,rax
       mov       r12,r9
       or        rdx,r10
       or        rdx,r11
       or        rdx,[rbp-0C8]
       or        rdx,[rbp-0D0]
       or        rdx,[rbp-0D8]
       or        rdx,r12
       and       r8,r9
       shr       r8,8
       and       r8,rax
       or        rdx,r8
       mov       r8,rsi
       not       r8
       mov       r9,0FEFEFEFEFEFEFEFE
       and       r9,rcx
       shr       r9,1
       and       r9,rax
       mov       r10,0FEFEFEFEFEFEFEFE
       and       r10,r9
       and       r10,r8
       shr       r10,1
       and       r10,rax
       mov       r11,0FEFEFEFEFEFEFEFE
       and       r11,r10
       and       r11,r8
       shr       r11,1
       and       r11,rax
       mov       [rbp-0E0],r11
       mov       r12,0FEFEFEFEFEFEFEFE
       and       r12,r11
       and       r12,r8
       shr       r12,1
       and       r12,rax
       mov       [rbp-0E8],r12
       mov       r11,0FEFEFEFEFEFEFEFE
       and       r11,r12
       and       r11,r8
       shr       r11,1
       and       r11,rax
       mov       r12,0FEFEFEFEFEFEFEFE
       and       r12,r11
       and       r12,r8
       shr       r12,1
       and       r12,rax
       or        rdx,r9
       or        rdx,r10
       or        rdx,[rbp-0E0]
       or        rdx,[rbp-0E8]
       or        rdx,r11
       or        rdx,r12
       mov       r9,0FEFEFEFEFEFEFEFE
       and       r9,r12
       and       r8,r9
       shr       r8,1
       and       r8,rax
       or        rdx,r8
       jne       near ptr M02_L36
M02_L12:
       blsr      rdi,rdi
       jne       near ptr M02_L11
M02_L13:
       mov       r12,[rbx+30]
       mov       rcx,[rbx+78]
       mov       [rbp-0F8],rcx
       mov       r10d,[rbp-9C]
       mov       [rbp-0FC],r10d
       test      r12,r12
       je        near ptr M02_L16
M02_L14:
       blsi      r9,r12
       mov       [rbp-0F0],r9
       mov       rdi,r9
       mov       rsi,rax
       mov       rdx,rcx
       call      qword ptr [7F3365546C10]; ChessEngine.Search.GenBishopMoves(UInt64, UInt64, UInt64)
       mov       r8,[rbp-0F8]
       mov       r9,r8
       not       r9
       mov       rdi,[rbp-0F0]
       mov       rcx,rdi
       shl       rcx,8
       mov       rdx,[rbp-48]
       and       rcx,rdx
       mov       rsi,rcx
       and       rcx,r9
       shl       rcx,8
       and       rcx,rdx
       mov       r10,rcx
       and       rcx,r9
       shl       rcx,8
       and       rcx,rdx
       mov       [rbp-108],rcx
       and       rcx,r9
       shl       rcx,8
       and       rcx,rdx
       mov       [rbp-110],rcx
       and       rcx,r9
       shl       rcx,8
       and       rcx,rdx
       mov       [rbp-118],rcx
       and       rcx,r9
       shl       rcx,8
       and       rcx,rdx
       mov       r11,rcx
       or        rsi,r10
       or        rsi,[rbp-108]
       or        rsi,[rbp-110]
       or        rsi,[rbp-118]
       or        rsi,r11
       and       r9,rcx
       shl       r9,8
       and       r9,rdx
       or        r9,rsi
       mov       rcx,r8
       not       rcx
       mov       rsi,7F7F7F7F7F7F7F7F
       and       rsi,rdi
       add       rsi,rsi
       and       rsi,rdx
       mov       r10,7F7F7F7F7F7F7F7F
       and       r10,rsi
       and       r10,rcx
       add       r10,r10
       and       r10,rdx
       mov       r11,7F7F7F7F7F7F7F7F
       and       r11,r10
       and       r11,rcx
       add       r11,r11
       and       r11,rdx
       mov       [rbp-120],r11
       mov       rdi,7F7F7F7F7F7F7F7F
       and       rdi,r11
       and       rdi,rcx
       add       rdi,rdi
       and       rdi,rdx
       mov       [rbp-128],rdi
       mov       r11,7F7F7F7F7F7F7F7F
       and       r11,rdi
       and       r11,rcx
       add       r11,r11
       and       r11,rdx
       mov       rdi,7F7F7F7F7F7F7F7F
       and       rdi,r11
       and       rdi,rcx
       add       rdi,rdi
       and       rdi,rdx
       or        r9,rsi
       or        r9,r10
       or        r9,[rbp-120]
       or        r9,[rbp-128]
       or        r9,r11
       or        r9,rdi
       mov       rsi,7F7F7F7F7F7F7F7F
       and       rdi,rsi
       and       rdi,rcx
       add       rdi,rdi
       and       rdi,rdx
       or        r9,rdi
       mov       rdi,r8
       not       rdi
       mov       rcx,[rbp-0F0]
       mov       rsi,rcx
       shr       rsi,8
       and       rsi,rdx
       mov       r10,rsi
       and       rsi,rdi
       shr       rsi,8
       and       rsi,rdx
       mov       [rbp-130],rsi
       and       rsi,rdi
       shr       rsi,8
       and       rsi,rdx
       mov       [rbp-138],rsi
       and       rsi,rdi
       shr       rsi,8
       and       rsi,rdx
       mov       [rbp-140],rsi
       and       rsi,rdi
       shr       rsi,8
       and       rsi,rdx
       mov       [rbp-148],rsi
       and       rsi,rdi
       shr       rsi,8
       and       rsi,rdx
       mov       r11,rsi
       or        r9,r10
       or        r9,[rbp-130]
       or        r9,[rbp-138]
       or        r9,[rbp-140]
       or        r9,[rbp-148]
       or        r9,r11
       and       rdi,rsi
       shr       rdi,8
       and       rdi,rdx
       or        r9,rdi
       mov       rdi,r8
       not       rdi
       mov       rsi,0FEFEFEFEFEFEFEFE
       and       rsi,rcx
       shr       rsi,1
       and       rsi,rdx
       mov       r10,0FEFEFEFEFEFEFEFE
       and       r10,rsi
       and       r10,rdi
       shr       r10,1
       and       r10,rdx
       mov       r11,0FEFEFEFEFEFEFEFE
       and       r11,r10
       and       r11,rdi
       shr       r11,1
       and       r11,rdx
       mov       [rbp-150],r11
       mov       rcx,0FEFEFEFEFEFEFEFE
       and       rcx,r11
       and       rcx,rdi
       shr       rcx,1
       and       rcx,rdx
       mov       [rbp-158],rcx
       mov       r11,0FEFEFEFEFEFEFEFE
       and       r11,rcx
       and       r11,rdi
       shr       r11,1
       and       r11,rdx
       mov       rcx,0FEFEFEFEFEFEFEFE
       and       rcx,r11
       and       rcx,rdi
       shr       rcx,1
       and       rcx,rdx
       or        r9,rax
       or        r9,rsi
       or        r9,r10
       or        r9,[rbp-150]
       or        r9,[rbp-158]
       or        r9,r11
       or        r9,rcx
       mov       rsi,0FEFEFEFEFEFEFEFE
       and       rcx,rsi
       and       rdi,rcx
       shr       rdi,1
       and       rdi,rdx
       or        r9,rdi
       jne       near ptr M02_L37
M02_L15:
       blsr      r12,r12
       mov       rax,rdx
       mov       rcx,r8
       jne       near ptr M02_L14
M02_L16:
       xor       r9d,r9d
       mov       [rsp],r9d
       mov       [rsp+8],r14
       mov       r8d,[rbp-0FC]
       mov       [rsp+10],r8d
       movzx     r8d,byte ptr [rbx+90]
       movzx     r9d,byte ptr [rbx+91]
       mov       rdi,[rbx+38]
       mov       rcx,[rbx+80]
       mov       rsi,rax
       mov       rdx,[rbp-38]
       call      qword ptr [7F3365546A90]; ChessEngine.Search.GetKingMoves(UInt64, UInt64, UInt64, UInt64, Boolean, Boolean, Player.PlayerEnum, ChessEngine.Move[], Int32)
       mov       r12d,eax
M02_L17:
       xor       eax,eax
       mov       [rbp-40],eax
       xor       ecx,ecx
       mov       [rbp-3C],r12d
       cmp       ecx,r12d
       jge       near ptr M02_L25
M02_L18:
       cmp       ecx,100
       jae       near ptr M02_L40
       mov       [rbp-2E0],rcx
       lea       rdi,[rcx+rcx*2]
       lea       rdx,[r14+rdi*8+10]
       mov       [rbp-2E8],rdx
       vmovdqu   xmm0,xmmword ptr [rdx]
       vmovdqu   xmmword ptr [rsp],xmm0
       mov       rdi,[rdx+10]
       mov       [rsp+10],rdi
       mov       rdi,rbx
       call      qword ptr [7F3365546AC0]; ChessEngine.Board.MakeMove(ChessEngine.Move)
       mov       rax,[rbp-2F0]
       mov       rdi,rax
       mov       rcx,[rdi]
       mov       [rbp-270],rcx
       mov       rdx,[rdi+8]
       mov       [rbp-278],rdx
       mov       rsi,[rdi+10]
       mov       [rbp-280],rsi
       mov       r8,[rdi+18]
       mov       [rbp-288],r8
       mov       r9,[rdi+20]
       mov       [rbp-290],r9
       mov       r10,[rdi+28]
       mov       [rbp-298],r10
       mov       r11,[rdi+30]
       mov       [rbp-2A0],r11
       mov       rax,[rdi+38]
       mov       rdx,[rdi+40]
       mov       [rbp-2A8],rdx
       mov       rsi,[rdi+48]
       mov       [rbp-2B0],rsi
       mov       r8,[rdi+50]
       mov       [rbp-2B8],r8
       mov       r12,[rdi+58]
       mov       rcx,[rdi+60]
       mov       [rbp-2C0],rcx
       mov       rdi,[rdi+68]
       mov       [rbp-2C8],rdi
       test      r13d,r13d
       jne       near ptr M02_L22
       mov       r9d,1
       mov       r13,r10
M02_L19:
       test      r9d,r9d
       je        near ptr M02_L23
       mov       r9,rdi
       not       r9
       mov       [rbp-168],r9
       mov       r10,7F7F7F7F7F7F7F7F
       and       r10,r11
       shr       r10,7
       mov       [rbp-178],r10
       mov       rdi,rax
       call      qword ptr [7F3365546B80]; ChessEngine.Search.GenAllKnightMoves(UInt64)
       mov       rcx,0FEFEFEFEFEFEFEFE
       and       rcx,[rbp-2A0]
       shr       rcx,9
       or        rcx,[rbp-178]
       or        rax,rcx
       mov       [rbp-2F8],rax
       mov       rdi,[rbp-2A8]
       mov       rsi,[rbp-168]
       mov       rdx,[rbp-2C0]
       call      qword ptr [7F3365546C10]; ChessEngine.Search.GenBishopMoves(UInt64, UInt64, UInt64)
       or        rax,[rbp-2F8]
       mov       [rbp-160],rax
       mov       rdi,[rbp-2B0]
       mov       rsi,[rbp-168]
       mov       rdx,[rbp-2C0]
       call      qword ptr [7F3365546C28]; ChessEngine.Search.GenRookMoves(UInt64, UInt64, UInt64)
       or        rax,[rbp-160]
       mov       [rbp-160],rax
       mov       rdi,[rbp-2B8]
       mov       rsi,[rbp-168]
       mov       rdx,[rbp-2C0]
       call      qword ptr [7F3365546C10]; ChessEngine.Search.GenBishopMoves(UInt64, UInt64, UInt64)
       or        rax,[rbp-160]
       mov       [rbp-2F8],rax
       mov       rdi,[rbp-2B8]
       mov       rsi,[rbp-168]
       mov       rdx,[rbp-2C0]
       call      qword ptr [7F3365546C28]; ChessEngine.Search.GenRookMoves(UInt64, UInt64, UInt64)
       or        rax,[rbp-2F8]
       mov       [rbp-2F8],rax
       mov       rdi,r12
       call      qword ptr [7F3365546B98]; ChessEngine.Search.GenAllBasicKingMoves(UInt64)
       or        rax,[rbp-2F8]
       mov       r12,rax
M02_L20:
       test      r12,r13
       jne       near ptr M02_L24
       mov       r13d,[rbp-40]
       lea       edi,[r13+1]
       mov       r12d,edi
       mov       rdi,[rbp-2E8]
       mov       rax,[rdi]
       mov       rcx,[rdi+8]
       mov       edi,[rdi+10]
       cmp       r13d,[r15+8]
       jae       near ptr M02_L40
       mov       edx,r13d
       lea       rdx,[rdx+rdx*2]
       lea       rdx,[r15+rdx*8+10]
       mov       [rdx],rax
       mov       [rdx+8],rcx
       mov       [rdx+10],edi
M02_L21:
       mov       rdi,[rbx+8]
       mov       eax,[rdi+10]
       dec       eax
       mov       rcx,[rdi+8]
       mov       edx,[rcx+8]
       cmp       edx,eax
       jbe       near ptr M02_L39
       inc       dword ptr [rdi+14]
       mov       [rdi+10],eax
       mov       edi,eax
       imul      rdi,88
       vmovdqu32 zmm0,[rcx+rdi+10]
       vmovdqu32 [rbp-200],zmm0
       vmovdqu32 zmm0,[rcx+rdi+50]
       vmovdqu32 [rbp-1C0],zmm0
       mov       rax,[rcx+rdi+90]
       mov       [rbp-180],rax
       mov       r13,[rbp-2F0]
       vmovdqu32 zmm0,[rbp-200]
       vmovdqu32 [r13],zmm0
       vmovdqu32 zmm0,[rbp-1C0]
       vmovdqu32 [r13+40],zmm0
       mov       rdi,[rbp-180]
       mov       [r13+80],rdi
       mov       rdi,[rbp-2E0]
       inc       edi
       mov       eax,[rbp-3C]
       cmp       edi,eax
       mov       [rbp-3C],eax
       mov       rcx,rdi
       jge       near ptr M02_L26
       mov       [rbp-40],r12d
       mov       r13d,[rbp-2C]
       jmp       near ptr M02_L18
M02_L22:
       xor       r9d,r9d
       mov       r13,r12
       jmp       near ptr M02_L19
M02_L23:
       mov       rcx,[rbp-2C0]
       mov       r12,rcx
       not       r12
       mov       rax,7F7F7F7F7F7F7F7F
       and       rax,[rbp-270]
       shl       rax,9
       mov       [rbp-170],rax
       mov       rdi,[rbp-278]
       call      qword ptr [7F3365546B80]; ChessEngine.Search.GenAllKnightMoves(UInt64)
       mov       rcx,0FEFEFEFEFEFEFEFE
       and       rcx,[rbp-270]
       shl       rcx,7
       or        rcx,[rbp-170]
       or        rax,rcx
       mov       [rbp-2F8],rax
       mov       rdi,[rbp-280]
       mov       rsi,r12
       mov       rdx,[rbp-2C8]
       call      qword ptr [7F3365546C10]; ChessEngine.Search.GenBishopMoves(UInt64, UInt64, UInt64)
       or        rax,[rbp-2F8]
       mov       [rbp-160],rax
       mov       rdi,[rbp-288]
       mov       rsi,r12
       mov       rdx,[rbp-2C8]
       call      qword ptr [7F3365546C28]; ChessEngine.Search.GenRookMoves(UInt64, UInt64, UInt64)
       or        rax,[rbp-160]
       mov       [rbp-160],rax
       mov       rdi,[rbp-290]
       mov       rsi,r12
       mov       rdx,[rbp-2C8]
       call      qword ptr [7F3365546C10]; ChessEngine.Search.GenBishopMoves(UInt64, UInt64, UInt64)
       or        rax,[rbp-160]
       mov       [rbp-2F8],rax
       mov       rdi,[rbp-290]
       mov       rsi,r12
       mov       rdx,[rbp-2C8]
       call      qword ptr [7F3365546C28]; ChessEngine.Search.GenRookMoves(UInt64, UInt64, UInt64)
       or        rax,[rbp-2F8]
       mov       [rbp-2F8],rax
       mov       rdi,[rbp-298]
       call      qword ptr [7F3365546B98]; ChessEngine.Search.GenAllBasicKingMoves(UInt64)
       or        rax,[rbp-2F8]
       mov       r12,rax
       jmp       near ptr M02_L20
M02_L24:
       mov       r12d,[rbp-40]
       jmp       near ptr M02_L21
M02_L25:
       mov       r12d,[rbp-40]
M02_L26:
       mov       eax,r12d
       vzeroupper
       add       rsp,2E8
       pop       rbx
       pop       r12
       pop       r13
       pop       r14
       pop       r15
       pop       rbp
       ret
M02_L27:
       mov       r11d,edx
       lea       edx,[r11+1]
       cmp       r11d,0FC
       ja        near ptr M02_L34
       lea       r11,[r11+r11*2]
       lea       r11,[r14+r11*8+10]
       mov       [r11],r8
       mov       [r11+8],r10
       mov       dword ptr [r11+10],1
       mov       r11d,edx
       lea       edx,[r11+1]
       lea       r11,[r11+r11*2]
       lea       r11,[r14+r11*8+10]
       mov       [r11],r8
       mov       [r11+8],r10
       mov       dword ptr [r11+10],2
       mov       r11d,edx
       lea       edx,[r11+1]
       lea       r11,[r11+r11*2]
       lea       r11,[r14+r11*8+10]
       mov       [r11],r8
       mov       [r11+8],r10
       mov       dword ptr [r11+10],3
       mov       r11d,edx
       lea       edx,[r11+1]
       lea       r11,[r11+r11*2]
       lea       r11,[r14+r11*8+10]
       mov       [r11],r8
       mov       [r11+8],r10
       mov       dword ptr [r11+10],4
       mov       r11d,edx
       jmp       near ptr M02_L32
M02_L28:
       mov       r11d,edx
M02_L29:
       blsr      rsi,rsi
       je        near ptr M02_L01
       mov       edx,r11d
M02_L30:
       blsi      r8,rsi
       mov       r9,r8
       shl       r9,8
       and       r9,r12
       mov       r10,7F7F7F7F7F7F7F7F
       and       r10,r8
       shl       r10,9
       and       r10,rdi
       mov       r11,r9
       and       r11,0FF0000
       shl       r11,8
       and       r11,r12
       or        r9,r11
       or        r9,r10
       mov       r10,0FEFEFEFEFEFEFEFE
       and       r10,r8
       shl       r10,7
       and       r10,rdi
       or        r9,r10
       je        short M02_L28
M02_L31:
       blsi      r10,r9
       mov       r11,0FF00000000000000
       test      r11,r10
       jne       near ptr M02_L27
       lea       r11d,[rdx+1]
       cmp       edx,100
       jae       near ptr M02_L40
       mov       edx,edx
       lea       rdx,[rdx+rdx*2]
       lea       rdx,[r14+rdx*8+10]
       mov       [rdx],r8
       mov       [rdx+8],r10
       xor       r10d,r10d
       mov       [rdx+10],r10d
M02_L32:
       blsr      r9,r9
       je        near ptr M02_L29
       mov       edx,r11d
       jmp       short M02_L31
M02_L33:
       mov       r10,[rbp-260]
       mov       rcx,r10
       not       rcx
       mov       [rbp-68],rcx
       mov       rdi,[rbp-208]
       call      qword ptr [7F3365546BF8]; ChessEngine.Search.GenWhitePawnAttacks(UInt64)
       mov       [rbp-2F8],rax
       mov       rdi,[rbp-210]
       call      qword ptr [7F3365546B80]; ChessEngine.Search.GenAllKnightMoves(UInt64)
       or        rax,[rbp-2F8]
       mov       [rbp-2F8],rax
       mov       rdi,[rbp-218]
       mov       rsi,[rbp-68]
       mov       rdx,[rbp-268]
       call      qword ptr [7F3365546C10]; ChessEngine.Search.GenBishopMoves(UInt64, UInt64, UInt64)
       or        rax,[rbp-2F8]
       mov       [rbp-2F8],rax
       mov       rdi,[rbp-220]
       mov       rsi,[rbp-68]
       mov       rdx,[rbp-268]
       call      qword ptr [7F3365546C28]; ChessEngine.Search.GenRookMoves(UInt64, UInt64, UInt64)
       or        rax,[rbp-2F8]
       mov       [rbp-58],rax
       mov       rdi,[rbp-228]
       mov       rsi,[rbp-68]
       mov       rdx,[rbp-268]
       call      qword ptr [7F3365546C40]; ChessEngine.Search.GenQueenMoves(UInt64, UInt64, UInt64)
       or        rax,[rbp-58]
       mov       [rbp-2F8],rax
       mov       rdi,[rbp-230]
       call      qword ptr [7F3365546B98]; ChessEngine.Search.GenAllBasicKingMoves(UInt64)
       or        rax,[rbp-2F8]
       jmp       near ptr M02_L00
M02_L34:
       cmp       r11d,100
       jae       near ptr M02_L40
       mov       r11d,r11d
       lea       r11,[r11+r11*2]
       lea       r11,[r14+r11*8+10]
       mov       [r11],r8
       mov       [r11+8],r10
       mov       dword ptr [r11+10],1
       mov       r11d,edx
       lea       edx,[r11+1]
       cmp       r11d,100
       jae       near ptr M02_L40
       lea       r11,[r11+r11*2]
       lea       r11,[r14+r11*8+10]
       mov       [r11],r8
       mov       [r11+8],r10
       mov       dword ptr [r11+10],2
       mov       r11d,edx
       lea       edx,[r11+1]
       cmp       r11d,100
       jae       near ptr M02_L40
       lea       r11,[r11+r11*2]
       lea       r11,[r14+r11*8+10]
       mov       [r11],r8
       mov       [r11+8],r10
       mov       dword ptr [r11+10],3
       mov       r11d,edx
       lea       edx,[r11+1]
       cmp       r11d,100
       jae       near ptr M02_L40
       lea       r11,[r11+r11*2]
       lea       r11,[r14+r11*8+10]
       mov       [r11],r8
       mov       [r11+8],r10
       mov       dword ptr [r11+10],4
       mov       r11d,edx
       jmp       near ptr M02_L32
M02_L35:
       blsi      rcx,rdx
       mov       r8d,[rbp-7C]
       lea       r9d,[r8+1]
       cmp       r8d,100
       jae       near ptr M02_L40
       lea       r8,[r8+r8*2]
       lea       r8,[r14+r8*8+10]
       mov       r10,[rbp-78]
       mov       [r8],r10
       mov       [r8+8],rcx
       xor       ecx,ecx
       mov       [r8+10],ecx
       blsr      rdx,rdx
       mov       [rbp-7C],r9d
       jne       short M02_L35
       jmp       near ptr M02_L09
M02_L36:
       blsi      r8,rdx
       mov       r9d,[rbp-9C]
       lea       r10d,[r9+1]
       cmp       r9d,100
       jae       near ptr M02_L40
       lea       r9,[r9+r9*2]
       lea       r9,[r14+r9*8+10]
       mov       [r9],rcx
       mov       [r9+8],r8
       xor       r8d,r8d
       mov       [r9+10],r8d
       blsr      rdx,rdx
       mov       [rbp-9C],r10d
       jne       short M02_L36
       jmp       near ptr M02_L12
M02_L37:
       blsi      rdi,r9
       mov       ecx,[rbp-0FC]
       lea       esi,[rcx+1]
       cmp       ecx,100
       jae       near ptr M02_L40
       lea       rcx,[rcx+rcx*2]
       lea       rcx,[r14+rcx*8+10]
       mov       rax,[rbp-0F0]
       mov       [rcx],rax
       mov       [rcx+8],rdi
       xor       edi,edi
       mov       [rcx+10],edi
       blsr      r9,r9
       mov       [rbp-0FC],esi
       jne       short M02_L37
       jmp       near ptr M02_L15
M02_L38:
       mov       rdx,[rbx+78]
       mov       r9,rdx
       not       r9
       mov       [rbp-50],r9
       mov       rdx,[rbx+70]
       or        rdx,[rbx+88]
       mov       rdi,[rbx+40]
       mov       rsi,r12
       mov       rcx,r14
       xor       r8d,r8d
       call      qword ptr [7F3365546AA8]
       mov       r12d,eax
       mov       rdi,[rbx+48]
       mov       rsi,[rbp-50]
       mov       rdx,r14
       mov       ecx,r12d
       call      qword ptr [7F3365546A30]; ChessEngine.Search.GetKnightMoves(UInt64, UInt64, ChessEngine.Move[], Int32)
       mov       r12d,eax
       mov       rdi,[rbx+50]
       mov       rdx,[rbx+70]
       mov       rsi,[rbp-50]
       mov       rcx,r14
       mov       r8d,r12d
       call      qword ptr [7F3365546A48]; ChessEngine.Search.GetBishopMoves(UInt64, UInt64, UInt64, ChessEngine.Move[], Int32)
       mov       r12d,eax
       mov       rdi,[rbx+58]
       mov       rdx,[rbx+70]
       mov       rsi,[rbp-50]
       mov       rcx,r14
       mov       r8d,r12d
       call      qword ptr [7F3365546A60]; ChessEngine.Search.GetRookMoves(UInt64, UInt64, UInt64, ChessEngine.Move[], Int32)
       mov       r12d,eax
       mov       rdi,[rbx+60]
       mov       rdx,[rbx+70]
       mov       rsi,[rbp-50]
       mov       rcx,r14
       mov       r8d,r12d
       call      qword ptr [7F3365546A78]; ChessEngine.Search.GetQueenMoves(UInt64, UInt64, UInt64, ChessEngine.Move[], Int32)
       mov       r12d,eax
       mov       [rsp],r13d
       mov       [rsp+8],r14
       mov       [rsp+10],r12d
       movzx     r8d,byte ptr [rbx+92]
       movzx     r9d,byte ptr [rbx+93]
       mov       rdi,[rbx+68]
       mov       rcx,[rbx+80]
       mov       rsi,[rbp-50]
       mov       rdx,[rbp-58]
       call      qword ptr [7F3365546A90]; ChessEngine.Search.GetKingMoves(UInt64, UInt64, UInt64, UInt64, Boolean, Boolean, Player.PlayerEnum, ChessEngine.Move[], Int32)
       mov       r12d,eax
       jmp       near ptr M02_L17
M02_L39:
       call      qword ptr [7F3365546EE0]
       int       3
M02_L40:
       call      CORINFO_HELP_RNGCHKFAIL
       int       3
; Total bytes of code 5339
```

