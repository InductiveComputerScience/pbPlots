.intel_syntax noprefix

.globl stosd
stosd:
	mov rdi, rdi
	mov rcx, rsi
	mov rax, rdx
	rep stosd
	ret
